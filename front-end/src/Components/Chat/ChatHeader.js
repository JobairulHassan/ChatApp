import { Box, Stack, useTheme, Typography} from "@mui/material";
import React, { useContext } from "react";
//import UserCard from "../UserCard";
import { ChatContext } from "../General/Provider/ChatProvider";
import HiddenSidebar from "../LeftSidebar/HiddenSidebar";
import HiddenUserInformation from "../UserInformation";

const ChatHeader = () => {
  const { selectedUser } = useContext(ChatContext);

  return (
    <Stack direction={"row"} justifyContent={"space-between"}>
      <Stack direction={"row"} spacing={1}>
        <HiddenSidebar />
        <Box display={{ xs: "none", sm: "flex" }} alignItems="flex-end" justifyContent="flex-start">
          <Typography variant="h6"> {selectedUser.firstName +" "+selectedUser.lastName}</Typography>
          {/* <UserCard user={selectedUser} /> */}
        </Box>
        <Box display={{ xs: "flex", sm: "none" }}>
          {/* <UserCard user={selectedUser} size="medium" /> */}
        </Box>
      </Stack>
      <HiddenUserInformation />
    </Stack>
  );
};

export default ChatHeader;
