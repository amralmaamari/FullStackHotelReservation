
interface IOccupancyCounterProps{
    label:string,
    value:number,
    onIncrement:()=>void,
    onDecrement:()=>void,
}
export default function OccupancyCounter({label,value,onIncrement,onDecrement}:IOccupancyCounterProps) {
  return (
    <div className="flex justify-between mt-2">
        <span>{value} {label} </span>
        <div className="flex justify-between items-center gap-7 border border-black rounded-xl py-2 px-4 bg-white shadow-md">
        <button type="button" onClick={onDecrement}>-</button>
        <span>{value}</span>
        <button type="button" onClick={onIncrement}>+</button>
    </div>
  </div>
  )
}
