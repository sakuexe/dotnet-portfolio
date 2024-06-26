/** @type {HTMLFormElement | null} */
const editForm =  document.querySelector("form#edit");

/**
 * @param {string} url
 * @returns {Promise<boolean>}
 */
async function saveChanges(url) {
  /** 
   * @typedef {Object} Error
   * @property {string} Field
   * @property {string[]} Messages
   */

  /** @type {Error[]} */
  let data;
  if (!editForm) {
    console.warn("Form not found, stupid");
    return false;
  }
  const formData = new FormData(editForm);
  try {
    const response = await fetch(url, {
      method: "POST",
      body: formData
    });

    if (response.ok) return true;
    data = await response.json();
    console.table(data);

  } catch (error) {
    console.error(error.message);
    const generalError = editForm.querySelector("span#general-error");

    if (!generalError) {
      console.warn("no span#general-error found");
      return false;
    }
    generalError.textContent = "An unknown error occurred while saving the changes.";
    return false;
  }

  // handle the errors
  data.forEach(error => {
    // get the field with the corresponding for tag to the error field
    const field = editForm.querySelector(`span[data-valmsg-for="${error.Field}"]`);
    const generalError = editForm.querySelector("span#general-error");

    if (!field && generalError) {
      generalError.textContent = error.Messages.join(", ");
      return;
    }
    if (!field) {
      console.warn(`No field found for ${error.Field}`);
      return;
    }
    field.textContent = error.Messages.join(", ");
  });
  return false;
}

editForm?.addEventListener("submit", async (e) => {
  e.preventDefault();
  // reset the errors on submit
  editForm.querySelectorAll("span").forEach(span => span.textContent = "");

  const success = await saveChanges(editForm.action);
  if (!success) return;

  // on a successful save, show a confirmation message
  const successMessage = document.createElement("div");
  successMessage.id = "success-message";
  successMessage.textContent = "Changes saved successfully";
  successMessage.classList.add("text-center", "w-full", "opacity-75", "italic");
  editForm.appendChild(successMessage);
  // wait for 500ms and then reload the page
  setTimeout(() => {
    window.location.reload();
  }, 500);
})

editForm?.addEventListener("change", () => {
  /** @type {Node | null} */
  const successMessge = editForm.querySelector("#success-message");
  if (!successMessge) return
  editForm.removeChild(successMessge)
});
