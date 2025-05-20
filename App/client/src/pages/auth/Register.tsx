import { useEffect, useState } from "react";
import Navbar from "../../components/layout/Navbar";
import ReservationTypes from "../../components/reservation/ReservationTypes";
import { z } from "zod";
import { useForm, SubmitHandler } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { SginupCredentials, useAuth } from "../../context/AuthContext";


const signInSchema = z.object({
  email: z.string().email("Invalid email address"),
  password: z.string().min(6, "Password must be at least 6 characters"),
});

const signUpSchema = z.object({
  username: z.string().min(5, "Username should be least 5 characters"),
  email: z.string().email("Invalid email address"),
  password: z.string().min(6, "Password must be at least 6 characters"),
  countryID: z.string().optional(),
  image: z.any().optional(),
  city: z.string().optional(),
});

type ISignInSchema = z.infer<typeof signInSchema>;
type ISignUpSchema = z.infer<typeof signUpSchema>;

export default function Register() {

  // Toggle between Sign In and Sign Up modes
  const [isSignUp, setIsSignUp] = useState<boolean>(false);
  const { login, signup,user } = useAuth();

  
  const {
    register: registerSignIn,
    handleSubmit: handleSubmitSignIn,
    formState: { errors: errorsSignIn },
    reset: resetSignIn,
  } = useForm<ISignInSchema>({
    resolver: zodResolver(signInSchema),
  });

  const {
    register: registerSignUp,
    handleSubmit: handleSubmitSignUp,
    formState: { errors: errorsSignUp },
    reset: resetSignUp,
    setValue,
  } = useForm<ISignUpSchema>({
    resolver: zodResolver(signUpSchema),
  });

  const onSubmitSignIn: SubmitHandler<ISignInSchema> = async (data) => {
   await login(data);
    
  };

  
  const onSubmitSignUp: SubmitHandler<ISignUpSchema> = async (data) => {
    const countryID = data.countryID ? parseInt(data.countryID, 10) : undefined;

    const formattedData: SginupCredentials = {
      username: data.username,
      email: data.email,
      password: data.password,
      countryID: isNaN(countryID) ? "" : countryID,
      image: previewImage?previewImage:"",
      city: data.city?data.city:""
  };
    
    await signup(formattedData);
  };



  const [previewImage, setPreviewImage] = useState<string | null>(null);
  // Handle file input changes for the image field in sign up mode
  const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) {
      setPreviewImage(URL.createObjectURL(file));
      setValue("image", event.target.files);
    }
  };

  useEffect(() => {
    resetSignIn();
    resetSignUp();
    setPreviewImage(null);
  }, [isSignUp, resetSignIn, resetSignUp]);


    
  return (
    <>
      <div className="bg-primary pt-1 px-4">
        
        <ReservationTypes />
      </div>

      <div className="max-w-md mx-auto p-4">
        <h2 className="text-xl font-bold mb-4">
          {isSignUp ? "Sign Up" : "Sign In"}
        </h2>
        {isSignUp ? (
          <form
            key="signup"
            onSubmit={handleSubmitSignUp(onSubmitSignUp)}
            className="space-y-4"
          >
            <div>
              <label htmlFor="username" className="block text-sm font-medium">
                Username
              </label>
              <input
                id="username"
                type="text"
                {...registerSignUp("username")}
                className="mt-1 block w-full border border-gray-300 rounded-md p-2"
              />
              {errorsSignUp.username && (
                <p className="text-red-500 text-sm">
                  {errorsSignUp.username.message}
                </p>
              )}
            </div>
            <div>
              <label
                htmlFor="email-signup"
                className="block text-sm font-medium"
              >
                Email
              </label>
              <input
                id="email-signup"
                type="email"
                {...registerSignUp("email")}
                className="mt-1 block w-full border border-gray-300 rounded-md p-2"
              />
              {errorsSignUp.email && (
                <p className="text-red-500 text-sm">
                  {errorsSignUp.email.message}
                </p>
              )}
            </div>
            <div>
              <label
                htmlFor="password-signup"
                className="block text-sm font-medium"
              >
                Password
              </label>
              <input
                id="password-signup"
                type="password"
                {...registerSignUp("password")}
                className="mt-1 block w-full border border-gray-300 rounded-md p-2"
              />
              {errorsSignUp.password && (
                <p className="text-red-500 text-sm">
                  {errorsSignUp.password.message}
                </p>
              )}
            </div>

            <div>
              <label htmlFor="country" className="block text-sm font-medium">
                Country (Optional)
              </label>
              <select
                id="country"
                {...registerSignUp("countryID")}

                className="mt-1 block w-full border border-gray-300 rounded-md p-2"
              >
                <option value="">Select country</option>
                <option value="1">USA</option>
                <option value="2">UK</option>
                <option value="3">Canada</option>
                <option value="4">Australia</option>
              </select>
            </div>

            <div>
              <label htmlFor="city" className="block text-sm font-medium">
                City (Optional)
              </label>
              <input
                id="city"
                type="text"
                {...registerSignUp("city")}
                className="mt-1 block w-full border border-gray-300 rounded-md p-2"
              />
            </div>
            <div>
              <label htmlFor="image" className="block text-sm font-medium">
                Image (Optional)
              </label>
              <input
                id="image"
                type="file"
                accept="image/*"
                onChange={handleImageChange}
                className="mt-1 block w-full"
              />
              {previewImage && (
                <img
                  src={previewImage}
                  alt="Preview"
                  className="mt-2 h-24 w-24 object-cover rounded-full"
                />
              )}
            </div>
            <button
              type="submit"
              className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700"
            >
              Sign Up
            </button>
          </form>
        ) : (
          <div>
            <form
              key="signin"
              onSubmit={handleSubmitSignIn(onSubmitSignIn)}
              className="space-y-4"
            >
              <div>
                <label
                  htmlFor="email-signin"
                  className="block text-sm font-medium"
                >
                  Email
                </label>
                <input
                  type="email"
                  id="email-signin"
                  className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                  {...registerSignIn("email")}
                />
                {errorsSignIn.email && (
                  <p className="text-red-500 text-sm">
                    {errorsSignIn.email.message}
                  </p>
                )}
              </div>
              <div>
                <label
                  htmlFor="password-signin"
                  className="block text-sm font-medium"
                >
                  Password
                </label>
                <input
                  type="password"
                  id="password-signin"
                  className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                  {...registerSignIn("password")}
                />
                {errorsSignIn.password && (
                  <p className="text-red-500 text-sm">
                    {errorsSignIn.password.message}
                  </p>
                )}
              </div>
              <button
                type="submit"
                className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700"
              >
                Sign In
              </button>
            </form>
          </div>
        )}
        <div className="mt-4 text-center">
          <button
            onClick={() => setIsSignUp((prev) => !prev)}
            className="text-blue-600 hover:underline"
          >
            {isSignUp
              ? "Already have an account? Sign In"
              : "Don't have an account? Sign Up"}
          </button>
        </div>
      </div>
    </>
  );
}
