import { createGlobalStyle } from 'styled-components'
import { Theme } from './Theme'

export const GlobalStyle = createGlobalStyle`
:root {
  --shadow-md: 0 2px 4px 0 rgb(12 0 46 / 4%);
  --shadow-lg: 0 10px 14px 0 rgb(12 0 46 / 6%);

  --z-sticky: 7777;
  --z-navbar: 8888;
  --z-drawer: 9999;
  --z-modal: 9999;
}

*,
*::before,
*::after {
  box-sizing: border-box;
}

body,
h1,
h2,
h3,
h4,
p,
figure,
blockquote,
dl,
dd {
  margin: 0;
}

ul[role='list'],
ol[role='list'] {
  list-style: none;
}

html:focus-within {
  scroll-behavior: smooth;
} 

html {
  -webkit-font-smoothing: antialiased;
  touch-action: manipulation;
  text-rendering: optimizelegibility;
  text-size-adjust: 100%;
  font-size: 62.5%;

  @media (max-width: 37.5em) {
    font-size: 50%;
  }

  @media (max-width: 48.0625em) {
    font-size: 55%;
  }

  @media (max-width: 56.25em) {
    font-size: 60%;
  }
}

/* Set core body defaults */


body {
  min-height: 100vh;
  line-height: 1.5;
  font-family: var(--font);
  color: ${({ theme }: { theme: Theme }) => theme.text};
  background-color: ${({ theme }: { theme: Theme }) => theme.background};
}

.background {
  background-color: ${({ theme }: { theme: Theme }) => theme.background};
}

.modal {
  background-color: ${({ theme }: { theme: Theme }) => theme.modalBackground};
}

.alert {
  color: ${({ theme }: { theme: Theme }) => theme.alertColor};
}

svg {
  color: ${({ theme }: { theme: Theme }) => theme.text};
}

/* A elements that don't have a class get default styles */
a:not([class]) {
  text-decoration-skip-ink: auto;
}

img,
picture {
  max-width: 100%;
  display: block;
}

input,
button,
textarea,
select {
  font: inherit;
}

@media (prefers-reduced-motion: reduce) {
  html:focus-within {
   scroll-behavior: auto;
  }
  
  *,
  *::before,
  *::after {
    animation-duration: 0.01ms !important;
    animation-iteration-count: 1 !important;
    transition-duration: 0.01ms !important;
    scroll-behavior: auto !important;
  }

}`
