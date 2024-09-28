import { BrowserRouter, Route, Routes } from "react-router-dom";
import SignInPage from "./Pages/SignIn";
import SignUpPage from "./Pages/SignUp";
import MyAlert from "./Components/General/AlertIndex";
import Home from "./Pages/Home";
import ProtectedRoute from "./Components/General/ProtectedRoute";
import { CssBaseline, ThemeProvider } from "@mui/material";
import { useContext } from "react";
import { ThemeStatusContext } from "./Components/General/Provider/ThemeStatusProvider";
import { darkTheme, lightTheme } from "./Components/General/Themes";

function App() {
  const { isDarkTheme } = useContext(ThemeStatusContext);
  return (
    <ThemeProvider theme={isDarkTheme ? darkTheme : lightTheme}>
      <BrowserRouter>
        <Routes>
          <Route
            index
            element={
              <ProtectedRoute>
                <Home />
              </ProtectedRoute>
            }
          />
          <Route path="sign-in" element={<SignInPage />} />
          <Route path="sign-up" element={<SignUpPage />} />
        </Routes>
      </BrowserRouter>
      <MyAlert />
      <CssBaseline />
    </ThemeProvider>
  );
}

export default App;
