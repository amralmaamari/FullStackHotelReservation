import { Link } from "react-router-dom";

export default function NotFound() {
  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-gradient-to-br from-indigo-50 via-slate-100 to-white p-4 text-center text-slate-700">
      <h1 className="text-8xl font-bold tracking-widest">404</h1>
      <p className="mt-4 text-lg">Sorry, page not found!</p>
      <Link
        to="/"
        className="mt-8 inline-block rounded-lg bg-indigo-600 px-6 py-3 text-white transition hover:bg-indigo-700"
      >
        Back to Home
      </Link>
    </div>
  );
}
