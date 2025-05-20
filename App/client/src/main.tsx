import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.tsx";
import { SearchProvider } from "./context/searchContext.tsx";
import { AuthProvider } from "./context/AuthContext.tsx";
import { Toaster } from 'react-hot-toast';
import { BrowserRouter } from "react-router-dom";        // ← Router وحيد هنا

createRoot(document.getElementById("root")!).render(
  <StrictMode>
     <BrowserRouter>
    <AuthProvider>
      <SearchProvider>
        <App />
        <Toaster position="top-right" reverseOrder={false} />

      </SearchProvider>
    </AuthProvider>
    </BrowserRouter>
  </StrictMode>
);
