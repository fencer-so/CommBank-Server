import CommBank from '../assets/images/commbank.svg'

type LogoProps = {
  height: number
  width: number
}

export default function Logo(props: LogoProps) {
  return <img src={CommBank} height={props.height} width={props.width} />
}
