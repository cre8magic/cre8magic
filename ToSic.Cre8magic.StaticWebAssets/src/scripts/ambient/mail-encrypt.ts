import { debug } from '../shared/constants';

export function initMailDecrypt() {

  if (debug) console.log('initMailDecrypt');

  /* mailencrypting */
  setTimeout(function () {
    const mailElements = document.querySelectorAll('[data-madr1]:not(.madr-done)');
    (Array.from(mailElements) as HTMLElement[]).forEach((mail, index) => {
      const maddr = mail.getAttribute('data-madr1') + '@' + mail.getAttribute('data-madr2') + '.' + mail.getAttribute('data-madr3');
      const linktext = mail.getAttribute('data-linktext') ? mail.getAttribute('data-linktext') : maddr;

      const a = document.createElement('a');
      a.setAttribute('href', `mailto:${maddr}`);
      // Ensure linktext is defined (fall back to an empty string if null)
      a.innerHTML = linktext || '';

      if (mail.parentElement) mail.parentElement.appendChild(a);
      mail.classList.add('madr-done');
      mail.style.display = 'none';
    });
  }, 500);
}