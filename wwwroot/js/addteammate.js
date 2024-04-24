// this solution was inspired by this stackoverflow answer
// https://stackoverflow.com/questions/72929553/how-to-create-object-that-contains-a-list-of-object-in-a-single-form#answer-72947622

/** @type {HTMLDivElement | null} */
const teammates = document.querySelector("#teammates")
/** @type {number} */
let teammatesAmount = document.querySelectorAll("#teammates > div").length;

const addbtn = document.querySelector("button#add-teammate");
addbtn?.addEventListener("click", () => {
  addTeammate();
})

/** @type {string[]} */
const teamProperties = [
  "Name",
  "Role",
  "Link"
]

function addTeammate() {
  const teammateInput = document.createElement("div");
  teammateInput.classList.add("flex", "flex-col");

  teamProperties.forEach((prop) => {
    /** @type {HTMLLabelElement} */
    const label = document.createElement("label");
    label.innerText = prop;
    label.htmlFor = `Team[${teammatesAmount}].${prop}`;
    teammateInput.appendChild(label);

    /** @type {HTMLInputElement} */
    const input = document.createElement("input");
    input.name = `Team[${teammatesAmount}].${prop}`;
    input.type = "text";
    input.classList.add("bg-transparent", "border-2", "border-primary-700");
    teammateInput.appendChild(input);
  });

  teammates?.insertBefore(teammateInput, null);
  teammatesAmount += 1;
}


const removebtn = document.querySelectorAll("button.remove-teammate");

removebtn?.forEach((button) => {
  button.addEventListener("click", () => {
    /** @type {HTMLElement | null} */
    const existingTeammate = button.parentElement;
    if (!existingTeammate) {
      console.error("No teammate of that button was found");
      return
    }
    existingTeammate.parentElement?.removeChild(existingTeammate);
  });
});
