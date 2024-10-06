import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import SignPageContainer from "../../Components/SignPagesContainer";
import MyTextField from "../../Components/Inputs/MyTextField";
//import MyPasswordInputField from "../../Components/Inputs/MyPasswordInputField";
import ButtonWithLoading from "../../Components/General/ButtonWithLoading";
import axios from "axios";
import AlertContext from "../../Components/General/Provider/AlertProvider";
import AuthContext from "../../Components/General/Provider/AuthProvider";
import { Typography } from "@mui/material";
import styled from "@emotion/styled";

const StyledTypography = styled(Typography)({
  color: "#4287f5c2",
  "&:hover": { cursor: "pointer" },
});

const SignInPage = () => {
  const navigate = useNavigate();
  const { openAlert } = useContext(AlertContext);
  const { login } = useContext(AuthContext);
  const [isLoading, setIsLoading] = useState(false);
  const [inputs, setInputs] = useState({
    email: "",
  });

  const [validationErrors, setValidationErrors] = useState({
    email: " ",
  });

  const onChange = (e) => {
    const { name, value } = e.target;
    value === ""
      ? setValidationErrors({
          ...validationErrors,
          [name]: "you must fill this field",
        })
      : setValidationErrors({ ...validationErrors, [name]: " " });

    setInputs({ ...inputs, [name]: value });
  };

  const isValidate =
    validationErrors.email === " " &&
    inputs.email !== "";

  const onSubmit = async (e) => {
    setIsLoading(true);
    await axios
      .post("http://localhost:7271/api/login", JSON.stringify(inputs), {
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      })
      .then((response) => {
        login(response.data);
        openAlert("success", "success login");
        navigate("/");
      })
      .catch((error) => {
        console.error(error);
        if (error.response) {
          var errorMessage = error.response.data.error;
          if (errorMessage === "no user with this email") {
            setValidationErrors({
              ...validationErrors,
              email: errorMessage,
            });
          }
        } else {
          openAlert("error", "there is problem");
        }
      });
    setIsLoading(false);
  };

  return (
    <SignPageContainer>
      <MyTextField
        label="email"
        name="email"
        onChange={onChange}
        value={inputs.email}
        validation={validationErrors.email}
      />
      
      <ButtonWithLoading
        isLoading={isLoading}
        onClick={onSubmit}
        label="Sign In"
        disabled={!isValidate}
      />
      <Typography>
        you don't have an account?{" "}
        <StyledTypography variant="span" onClick={() => navigate("/sign-up")}>
          sign up
        </StyledTypography>
      </Typography>
    </SignPageContainer>
  );
};

export default SignInPage;
