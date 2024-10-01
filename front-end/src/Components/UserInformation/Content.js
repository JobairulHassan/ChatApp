import { Box, IconButton, Stack, Typography } from "@mui/material";
import React, { useContext } from "react";
import StyledHtmlText from "../General/StyledHtmlText";
import { ChatContext } from "../General/Provider/ChatProvider";
import styled from "@emotion/styled";
import CloseIcon from "@mui/icons-material/Close";

const Content = ({ closeDrawer }) => {
  const { selectedUser } = useContext(ChatContext);

  const StyledTypography = styled(Typography)({
    fontSize: "18px",
    fontWeight: "bold",
  });

  const StyledBox = styled(Box)({
    border: "1px solid silver",
    width: "90%",
    padding: "15px",
    borderRadius: "10px",
  });

  console.log(selectedUser);

  return (
    <Stack alignItems={"center"} spacing={2} width="300px">
      <IconButton
        type="button"
        sx={{ marginRight: "10px", alignSelf: "start" }}
        onClick={closeDrawer}
      >
        <CloseIcon sx={{ fontSize: "30px" }} />
      </IconButton>
      {/* <img
        style={{ borderRadius: "50%", objectFit: "cover" }}
        width={"220px"}
        height={"220px"}
        alt=""
        src={
          selectedUser.image !== null && selectedUser.image.imagePath !== null
            ? selectedUser.image.imagePath
            : "/Assets/defaultAvatar.png"
        }
      /> */}
      <StyledTypography>{selectedUser.firstName +" "+selectedUser.lastName}</StyledTypography>
      <StyledBox title="Contact Info.">
        <StyledHtmlText text={selectedUser.email} />
      </StyledBox>
      <StyledBox title="About">
        <StyledHtmlText text={selectedUser.about} />
      </StyledBox>
      
    </Stack>
  );
};

export default Content;
