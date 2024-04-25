const mockFields = document.querySelectorAll('input.string-list');
const containers = document.querySelectorAll('.string-list-container');
const deleteButtons = document.querySelectorAll('.string-list-container button');

mockFields?.forEach((field, index) => {
  field.addEventListener("change", () => {
    const values = splitStrings(/** @type {HTMLInputElement}*/(field));
    values.map((value) => addInputField(value, containers[index]));
  });
});

deleteButtons?.forEach((button) => {
  button.addEventListener("click", () => {
    const div = button.parentElement;
    if (!div) {
      console.error("no outer div for the button was found");
      return
    }
    div.remove();
  });
});

/** 
 * @param {HTMLInputElement} field
 * @return {string[]}
 */
function splitStrings(field) {
  let valueList = field.value.split(",");
  valueList = valueList.map((text) => text.trim().toLowerCase())
  return valueList
};

/** 
 * @param {string} value
 * @param {Element} currentContainer  
 */
function addInputField(value, currentContainer) {
  const deleteIcon = "x";
  const count = currentContainer.querySelectorAll("input").length;
  const currentProperty = currentContainer.id;

  if (!currentProperty) {
    console.error("id of property not set to", currentContainer)
    return
  }

  // add the invisible input field to the container
  const input = document.createElement("input");
  input.name = `${currentProperty}[${count}]`
  input.value = value
  input.hidden = true

  /// add a button element to display the values
  const button = document.createElement("button");
  button.textContent = `${value} ${deleteIcon}`;
  button.classList.add("bg-primary-600", "px-2", "py-1");

  currentContainer?.appendChild(input);
  currentContainer?.appendChild(button);
}
