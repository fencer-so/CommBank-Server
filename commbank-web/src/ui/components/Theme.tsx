export type Theme = {
  background: string
  secondBackground: string
  text: string
  textSecondary: string
  primary: string
  secondary: string
  tertiary: string
  cardBackground: string
  inputBackground: string
  navbarBackground: string
  modalBackground: string
  errorColor: string
  logoColor: string
  alertColor: string
  boxShadow: string
  overlay: string
}

export const LightTheme: Theme = {
  background: 'rgb(251,251,253)',
  secondBackground: 'rgb(255,255,255)',
  text: 'rgb(10,18,30)',
  textSecondary: 'rgb(255,255,255)',
  primary: 'rgb(22,115,255)',
  secondary: 'rgb(10,18,30)',
  tertiary: 'rgb(231,241,251)',
  cardBackground: 'hsla(0, 100%, 100%, 0.09)',
  inputBackground: 'rgb(255,255,255)',
  navbarBackground: 'rgb(255,255,255)',
  modalBackground: 'rgb(251,251,253)',
  errorColor: 'rgb(207,34,46)',
  logoColor: '#000',
  alertColor: 'rgba(155, 0, 50, 1)',
  boxShadow: `
    0px 12px 17px 2px hsla(0,0%,0%,0.14), 
    0px 5px 22px 4px hsla(0,0%,0%,0.12), 
    0px 7px 8px -4px hsla(0,0%,0%,0.2);`,
  overlay: 'rgba(0,0,0,0.4)',
}

export const DarkTheme: Theme = {
  background: 'rgba(18, 18, 18, 1)',
  secondBackground: 'rgb(45,55,72)',
  text: 'rgb(237,237,238)',
  textSecondary: 'rgb(255,255,255)',
  primary: '22,115,255',
  secondary: 'rgb(10,18,30)',
  tertiary: 'rgb(231,241,251)',
  cardBackground: 'hsla(0, 100%, 100%, 0.09)',
  inputBackground: 'rgb(45,55,72)',
  navbarBackground: 'rgb(45,55,72)',
  modalBackground: 'rgb(39, 39, 39)',
  errorColor: 'rgb(207,34,46)',
  logoColor: '#fff',
  alertColor: 'rgba(155, 0, 50, 1)',
  boxShadow: `
    0px 12px 17px 2px hsla(0,0%,0%,0.14), 
    0px 5px 22px 4px hsla(0,0%,0%,0.12), 
    0px 7px 8px -4px hsla(0,0%,0%,0.2);`,
  overlay: 'rgba(0,0,0,0.7)',
}
