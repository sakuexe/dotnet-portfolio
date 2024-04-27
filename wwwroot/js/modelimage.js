const removeButtons = document.querySelectorAll('.remove-image');

removeButtons.forEach((button) => {
  button.addEventListener('click', () => {
    // @ts-ignore
    const imageName = button.dataset.imagename;
    if (!imageName) {
      console.error('No image id found for:', button);
      return;
    }
    /** @type {HTMLInputElement | null} */
    const imageField = document.querySelector(`input[name="${imageName}"]`);
    if (!imageField) {
      console.error('No imageField was found with id:', imageName);
      return;
    }
    imageField.value = '';
    button.remove();
  });
});
