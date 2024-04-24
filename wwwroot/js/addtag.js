const newTags = document.querySelector("#new-tags");
/** @type {number} */
let tagCount = document.querySelectorAll("#tags > *").length;

function addTag() {
  const tagInput = document.createElement("input");
  tagInput.classList.add("bg-transparent", "border-2", "border-primary-700");
  tagInput.name = `Tags[${tagCount}]`;
  tagInput.placeholder = "New tag name...";

  newTags?.appendChild(tagInput);
  tagCount += 1;
}

const addTagBtn = document.querySelector("button#add-tag");
addTagBtn?.addEventListener("click", () => {
  addTag();
});

const removeTagBtns = document.querySelectorAll("button.remove-tag");
removeTagBtns?.forEach((button) => {
  const tag = button.parentElement
  if (!tag) {
    console.error("no tag for the button was found");
    return
  }
  button.addEventListener("click", () => {
    tag.parentElement?.removeChild(tag);
  })
})
