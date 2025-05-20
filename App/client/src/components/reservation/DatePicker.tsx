import { DateRange } from "react-date-range";
import "react-date-range/dist/styles.css"; // main css file
import "react-date-range/dist/theme/default.css"; // theme css file

interface DatePickerProps {
  selectionRange: {
    startDate: Date;
    endDate: Date;
    key: string;
  };
  onChange: (range: { startDate: Date; endDate: Date; key: string }) => void;
}

export default function DatePicker({ selectionRange, onChange }: DatePickerProps) {
  
  return (
    <DateRange
      editableDateInputs={true}
      onChange={(item) => onChange(item.selection)}
      moveRangeOnFirstSelection={false}
      ranges={[selectionRange]}
      minDate={new Date()}
      className="absolute top-[50px] -left-0 z-20"
    />
  );
}
