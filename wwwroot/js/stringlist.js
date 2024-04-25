const mockFields = document.querySelectorAll('input.string-list');
const containers = document.querySelectorAll('.string-list-container');
const deleteButtons = document.querySelectorAll('.string-list-container button');

mockFields?.forEach((field, index) => {
  field.addEventListener("change", () => {
    const values = splitStrings(/** @type {HTMLInputElement}*/(field));
    values.map((value) => addInputField(value, containers[index]));
    // @ts-ignore
    field.value = "";
  });
});

deleteButtons?.forEach((button) => {
  addRemoveButtonFunctionality(button);
});

/** @param {Element} button */
function addRemoveButtonFunctionality(button) {
  button.addEventListener("click", () => {
    const div = button.parentElement;
    const propertyGroup = div?.parentElement;
    if (!div) {
      console.error("the following button has no parent element", button);
      return
    }
    div.remove();
    reCountInputs(propertyGroup);
  });
}

/** @param {HTMLElement | null | undefined} element */
function reCountInputs(element) {
  if (!element) {
    console.error("no parent element found for input fields");
    return
  }
  const inputs = element.querySelectorAll("input");
  inputs.forEach((input, index) => {
    input.name = `${input.name.replace(/\[\d+\]/, `[${index}]`)}`
  });
}

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
  button.type = "button"; // prevent form submission

  // add the input and the button elements to a div
  // so that they are grouped together for easy removal
  const div = document.createElement("div");
  div.appendChild(input);
  div.appendChild(button);

  currentContainer.appendChild(div);
  addRemoveButtonFunctionality(button);
}
