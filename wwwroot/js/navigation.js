const navigation = document.querySelector('nav');
const toggleButton = navigation?.querySelector('button');
const dropdown = navigation?.querySelector('.dropdown-menu');
const navLinks = navigation?.querySelectorAll('a');

toggleButton?.addEventListener('click', () => {
  dropdown?.classList.toggle('hidden');
  toggleButtonAnimation();
});

navLinks?.forEach((link) => {
  link.addEventListener('click', () => {
    dropdown?.classList.toggle('hidden');
    toggleButtonAnimation();
  });
});

function toggleButtonAnimation() {
  const bars = toggleButton?.querySelectorAll('div');
  if (!bars || bars.length < 3) return;
  bars?.forEach((bar) => {
    bar.classList.toggle("absolute");
    bar.classList.toggle("inset-0");
    bar.classList.toggle("m-auto");
    bar.classList.toggle("w-3/4");
  });
  // hide the middle bar
  bars[1].classList.toggle("opacity-0");
  bars[1].classList.toggle("delay-100");
  // rotate the other bars 45 degrees
  bars[0].classList.toggle("rotate-45");
  bars[0].classList.toggle("w-full");
  bars[2].classList.toggle("-rotate-45");
}
