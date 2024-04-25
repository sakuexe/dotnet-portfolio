const mockFields = document.querySelectorAll('input.string-list');
const containers = document.querySelectorAll('.string-field-container');

mockFields?.forEach((field, index) => {
  field.addEventListener("change", () => {
    // @ts-ignore
    splitStrings(field, index);
  });
});

/** 
 * @param {HTMLInputElement} field
 * @param {number} index
 */
function splitStrings(field, index) {
  let valueList = field.value.split(",");
  valueList = valueList.map((text) => text.trim().toLowerCase())
  console.table(valueList);

  const currentContainer = /** @type {HTMLDivElement} */ (containers[index]);
  console.log(currentContainer);
  valueList.map((value, index) => addInputField(value, currentContainer, index + 1));
};

// when the skills is split, create input fields for the values
/** 
 * @param {string} value
 * @param {HTMLDivElement} currentContainer  
 * @param {number} count
 */
function addInputField(value, currentContainer, count) {
  const currentProperty = currentContainer.dataset["property"]
  if (!currentProperty) {
    console.error("data-property not set to", currentContainer)
    return
  }
  console.log(currentContainer, currentProperty);

  const input = document.createElement("input");
  input.name = `${currentProperty}[${count}]`
  input.value = value

  currentContainer?.appendChild(input);
}
