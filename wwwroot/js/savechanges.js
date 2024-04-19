/** @type {HTMLFormElement | null} */
const editForm = document.querySelector("form#edit");

/**
 * @param {string} url
 * @returns {Promise<boolean>}
 */
async function saveChanges(url) {
  /** 
   * @typedef {Object} Errors
   * @property {string} Field
   * @property {string[]} Messages
   */

  /** @type {Errors[]} */
  let data;
  if (!editForm) {
    console.error("Form not found");
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
    console.log(data);
  } catch (error) {
    const generalError = editForm.querySelector("span#general-error");
    if (!generalError) {
      console.error(error.message);
      return false;
    }
    generalError.textContent = "An unknown error occurred while saving the changes.";
    generalError.textContent += error.message;
    return false;
  }

  // handle the errors
  data.forEach(error => {
    // get the field with the corresponding for tag to the error field
    const field = editForm.querySelector(`span[data-valmsg-for="${error.Field}"]`);
    if (field) {
      field.textContent = error.Messages.join(", ");
    }
  });
  return false;
}

editForm?.addEventListener("submit", async (e) => {
  e.preventDefault();
  editForm.querySelectorAll("span").forEach(span => span.textContent = "");
  const success = await saveChanges(editForm.action);
  if (!success) {
    console.error("Failed to save changes");
    return;
  }
  console.log("Changes saved successfully");
})
