/** @type {NodeListOf<HTMLInputElement>}*/
const passwordInputs = document.querySelectorAll("input[type='password']");

passwordInputs.forEach((input) => {
  const toggleBtn = /** @type {HTMLButtonElement | null} */ (input.nextElementSibling);
  if (!toggleBtn) {
    console.error("no toggle button for", input, "was found");
    return
  }
  toggleBtn.addEventListener("click", () => {
    input.type = input.type === "password" ? "text" : "password";
    toggleBtn.innerText = toggleBtn.innerText === "👀" ? "🕶️" : "👀";
  });
});
