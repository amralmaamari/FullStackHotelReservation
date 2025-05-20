import { JSX } from "react"

interface IErrorProps{
    error:string
}
export default function Error({error}:IErrorProps):JSX.Element {
  return (
    <div className="col-span-full flex items-center gap-3 bg-red-100 border border-red-300 text-red-700 px-4 py-3 rounded-md">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="h-5 w-5 text-red-500"
                viewBox="0 0 20 20"
                fill="currentColor"
              >
                <path
                  fillRule="evenodd"
                  d="M18 10A8 8 0 11 2 10a8 8 0 0116 0zm-8.75-3.75a.75.75 0 011.5 0v3.5a.75.75 0 01-1.5 0v-3.5zm.75 7a.875.875 0 100-1.75.875.875 0 000 1.75z"
                  clipRule="evenodd"
                />
              </svg>
              <div>
                <p className="font-semibold">Oops! Something went wrong.</p>
                <p className="text-sm">
                  {error || "Unable to load data. Please try again later."}
                </p>
              </div>
            </div>
  )
}
