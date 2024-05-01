const image = document.querySelector('#portfolio-image');
const expandButton = document.querySelector('button#expand');

expandButton?.addEventListener('click', () => {
  toggleExpand();
  expandButton.textContent = expandButton.textContent === 'Expand'
    ? 'Collapse'
    : 'Expand';
});

function toggleExpand() {
  if (!image || !image.parentElement) {
    console.error('#portfolio-image or its parent element was not found');
    return;
  }
  image?.parentElement?.classList.toggle('max-h-[1440px]');
  image?.classList.toggle('object-cover');
}
