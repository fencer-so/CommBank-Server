import DateFnsUtils from '@date-io/date-fns'
import { KeyboardDatePicker, MuiPickersUtilsProvider } from '@material-ui/pickers'
import { MaterialUiPickersDate } from '@material-ui/pickers/typings/date'
import 'date-fns'
import React from 'react'

type Props = { value: Date | null; onChange: (date: MaterialUiPickersDate) => void }
export default function DatePicker(props: Props) {
  return (
    <MuiPickersUtilsProvider utils={DateFnsUtils}>
      <KeyboardDatePicker
        disableToolbar
        format="MM/dd/yyyy"
        margin="dense"
        id="date-picker-inline"
        value={props.value}
        onChange={props.onChange}
        style={{ width: 160 }}
        InputProps={{
          disableUnderline: true,
          style: {
            fontSize: '1.8rem',
            fontWeight: 'bold',
          },
        }}
      />
    </MuiPickersUtilsProvider>
  )
}
