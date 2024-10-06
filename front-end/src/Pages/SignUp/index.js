import React, { useContext, useState } from "react";
import SignPageContainer from "../../Components/SignPagesContainer";
import MyTextField from "../../Components/Inputs/MyTextField";
///import MyPasswordInputField from "../../Components/Inputs/MyPasswordInputField";
import ButtonWithLoading from "../../Components/General/ButtonWithLoading";
import { Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import AlertContext from "../../Components/General/Provider/AlertProvider";
import styled from "@emotion/styled";
import axios from "axios";

const StyledTypography = styled(Typography)({
  color: "#4287f5c2",
  "&:hover": { cursor: "pointer" },
});

const SignUpPage = () => {
  const navigate = useNavigate();
  const { openAlert } = useContext(AlertContext);
  const [isLoading, setIsLoading] = useState(false);
  const [inputs, setInputs] = useState({
    firstname: "",
    lastname: "",
    email: "",
  });

  const [validationErrors, setValidationErrors] = useState({
    firstname: " ",
    lastname: " ",
    email: " ",
  });

  const onChange = (e) => {
    const { name, value } = e.target;
    const validationErrorsCopy = { ...validationErrors };

    if (name === "firstname") {
      const firstnameRegex = /^[A-Za-z][A-Za-z]*$/;
      if (!value) {
        validationErrorsCopy[name] = "you must fill this field.";
      } else if (!firstnameRegex.test(value)) {
        validationErrorsCopy[name] =
          "please use letters. Only small and capital letters are allowed.";
      } else {
        validationErrorsCopy[name] = " ";
      }
    }

    if (name === "lastname") {
      const lastnameRegex = /^[A-Za-z][A-Za-z]*$/;
      if (!value) {
        validationErrorsCopy[name] = "you must fill this field.";
      } else if (!lastnameRegex.test(value)) {
        validationErrorsCopy[name] =
          "please use letters. Only small and capital letters are allowed.";
      } else {
        validationErrorsCopy[name] = " ";
      }
    }

    if (name === "email") {
      const emailRegex = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;
      if (!value) {
        validationErrorsCopy[name] = "you must fill this field.";
      } else if (!emailRegex.test(value)) {
        validationErrorsCopy[name] =
          "please use a valid email";
      } else {
        validationErrorsCopy[name] = " ";
      }
    }

    // if (name === "password") {
    //   if (!value) {
    //     validationErrorsCopy[name] = "you must fill this field.";
    //   } else if (value.length < 8) {
    //     validationErrorsCopy[name] =
    //       "password must be at least 8 characters long.";
    //   } else {
    //     validationErrorsCopy[name] = " ";
    //   }
    // }

    setValidationErrors(validationErrorsCopy);
    setInputs({ ...inputs, [name]: value });
  };

  // const CheckPasswordValidation = () => {
  //   if (
  //     inputs.password !== inputs.confirmPassword &&
  //     inputs.password !== "" &&
  //     inputs.confirmPassword !== ""
  //   ) {
  //     setValidationErrors({
  //       ...validationErrors,
  //       confirmPassword: "passwords not matched",
  //     });
  //     return false;
  //   }
  //   return true;
  // };

  const isValidate =
    validationErrors.firstname === " " &&
    validationErrors.lastname === " " &&
    validationErrors.email === " " &&
    inputs.firstname !== "" &&
    inputs.lastname !== "" &&
    inputs.email !== "";

  const onSubmit = async (e) => {

    setIsLoading(true);
    await axios
      .post(
        "http://localhost:7271/api/registration",
        JSON.stringify(inputs),
        {
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },
        }
      )
      .then((response) => {
        openAlert("success", "success registration");
        navigate("/sign-in");
      })
      .catch((error) => {
        if (error.response) {
          var errorMessage = error.response.data.error;
          if (errorMessage === "this email is already exist") {
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
        label="firstname"
        name="firstname"
        onChange={onChange}
        value={inputs.firstname}
        validation={validationErrors.firstname}
      />
      <MyTextField
        label="lastname"
        name="lastname"
        onChange={onChange}
        value={inputs.lastname}
        validation={validationErrors.lastname}
      />
      <MyTextField
        label="email"
        name="email"
        onChange={onChange}
        value={inputs.email}
        validation={validationErrors.email}
      />
      {/* <MyPasswordInputField
        name={"password"}
        label={"password"}
        value={inputs.password}
        onChange={onChange}
        validation={validationErrors.password}
      />
      <MyPasswordInputField
        name={"confirmPassword"}
        label={"confirm password"}
        value={inputs.confirmPassword}
        onChange={onChange}
        validation={validationErrors.confirmPassword}
      /> */}
      <ButtonWithLoading
        isLoading={isLoading}
        onClick={onSubmit}
        label="Sign Up"
        disabled={!isValidate}
      />
      <Typography>
        you already have account?{" "}
        <StyledTypography variant="span" onClick={() => navigate("/sign-in")}>
          sign in
        </StyledTypography>
      </Typography>
    </SignPageContainer>
  );
};

export default SignUpPage;
