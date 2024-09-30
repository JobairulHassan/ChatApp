import { Stack, Tooltip } from "@mui/material";
import React from "react";
import GetTime from "../../General/GetTime";

const SendedMessage = ({ message, index, currentUserId }) => {
  const isDeletedBySender = message.isDeleted === message.senderId;
  //const isDeletedByReceiver = message.isDeleted === message.receiverId;
  //console.log('currentuserID: '+currentUserId+ "  DeletedId: "+message.IsDeleted);
  return (
    <Tooltip title={GetTime(message.creationDate)} placement="left" arrow>
      <Stack
        key={index}
        p={1}
        borderRadius={"15px"}
        color={"white"}
        bgcolor={isDeletedBySender ? "#7d7d7d" : "#4287f5"}
        alignSelf={"end"}
        maxWidth={{ xs: "270px", sm: "400px", md: "500px", lg: "700px" }}
        sx={{
          wordWrap: "break-word",
          overflowWrap: "break-word",
          //display: isDeletedByReceiver ? 'none' : 'block', // Hide deleted messages
        }}
      >
        {isDeletedBySender ? (
          <span style={{ color: '#dbdbdb', fontStyle: 'italic' }}>Message removed</span>
        ) : (
          message.textBody
        )}
      </Stack>
    </Tooltip>
  );
};

export default SendedMessage;
