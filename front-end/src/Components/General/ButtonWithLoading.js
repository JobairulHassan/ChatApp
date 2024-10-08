import styled from "@emotion/styled";
import { Button, CircularProgress } from "@mui/material";
import React from "react";

const StyledButton = styled(Button)({
  height: "42px",
  textTransform: "none",
  background: "#4287f5c2",
  "&:hover": {
    background: "#4287f5",
  },
});

const ButtonWithLoading = ({ isLoading, onClick, label, disabled }) => {
  return (
    <StyledButton
      variant="contained"
      size="large"
      onClick={onClick}
      disabled={isLoading || disabled}
    >
      {isLoading ? (
        <CircularProgress
          color="inherit"
          size={16}
          sx={{ marginRight: "5px" }}
        />
      ) : (
        label
      )}
    </StyledButton>
  );
};

export default ButtonWithLoading;
