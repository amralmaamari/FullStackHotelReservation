import {
  createContext,
  useContext,
  useState,
  ReactNode,
  ReactElement,
} from "react";
import axios from "axios";
import toast from "react-hot-toast";
import { useLocation, useNavigate } from "react-router-dom";

interface LoginCredentials {
  email: string;
  password: string;
}

export interface SginupCredentials {
    username: string;
    email: string;
    password: string;
    countryID?: string | number | undefined;
    image?: string | null;
    city?: string | null;
}

interface User {
    username?: string;
    email: string;
    countryID?:number;
    image?:string;
    city?:string;
    // Add other relevant user details that your application might need
  }

interface AuthContextType {
user: User | null; // User type could be a defined interface based on what your backend sends
login: (credentials: LoginCredentials) => Promise<void>;
signup: (credentials: SginupCredentials) => Promise<void>;
  logout: () => void;
  error: string;
  loading: boolean;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({
  children,
}: {
  children: ReactNode;
}): ReactElement => {
  const navigate = useNavigate();
  const location = useLocation();
  const from = (location.state as any)?.from || '/';
  const backendUrl = import.meta.env.VITE_API_URL; // Ensure your environment variable is set
  const [user, setUser] = useState<User | null>(null);
  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);

  const signup = async (credentials: SginupCredentials) => {
    setLoading(true);
    setError("");
    try {
      const response = await axios.post(
        `${backendUrl}/Auth/register`,
        credentials
      );
      

      //here i stoped when the user signup so what after that 
      if (response.data.success) {
        setUser(response.data.data);
        toast.success("Signup successful!");
        navigate(from, { replace: true });
        
      } else {
        toast.error("Signup failed, please check your credentials.");
        setError("Invalid credentials");
      }
    } catch (error:any) {
      const errorMessage =
        error.response?.data?.message ||
        error.message ||
        "An unexpected error occurred";
        toast.error(errorMessage);
        setError(errorMessage)
      return false;
    } finally {
      setLoading(false);
    }
  };

  const login = async (credentials: LoginCredentials) => {
    setLoading(true);
    setError(""); // Clear any existing errors
    try {
      const response = await axios.post(
        `${backendUrl}/Auth/login`,
        credentials
      );
      if (response.data.success) {
        setUser(response.data.data);
        toast.success(`Welcome back, ${response.data.data.username}!`);
        navigate(from, { replace: true });

        // Here you might want to handle authentication tokens or session management
      } else {
        toast.error("Login failed, please check your credentials.");
        setError("Invalid credentials");
      }
    } catch (error: any) {
      const errorMessage =
        error.response?.data?.message ||
        error.message ||
        "An unexpected error occurred";
      console.error("Login error:", errorMessage);
      setError(errorMessage);
    } finally {
      setLoading(false);
    }
  };

  const logout = async () => {
    setLoading(true);
    try {
      const response = await axios.post(`${backendUrl}/Auth/logout`);
      if (response.data.success) {
        setUser(null);  // Clear the user state
        toast.success("Logout successful!");
      } else {
        alert("Logout failed.");
        setError("Logout failed");
      }
    } catch (error: any) {
      console.error("Logout error:", error.message);
      setError(error.message || "An unexpected error occurred");
    } finally {
      setLoading(false);
    }
  };

  const value = { user, login,signup, logout, error, loading };


  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
