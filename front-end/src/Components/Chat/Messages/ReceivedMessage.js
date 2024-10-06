import { Stack, Tooltip } from "@mui/material";
import React from "react";
import GetTime from "../../General/GetTime";

const ReceivedMessage = ({ message, index, currentUserId }) => {
  const isDeletedBySender = message.isDeleted === message.senderId;
  const isDeletedByUser = message.isDeleted === currentUserId;
  return (
    <Tooltip title={GetTime(message.creationDate)} placement="right" arrow>
      <Stack
        key={index}
        p={1}
        borderRadius={"15px"}
        bgcolor={isDeletedBySender ? "#7d7d7d" : "#75757550"}
        alignSelf={"start"}
        maxWidth={{ xs: "270px", sm: "400px", md: "500px", lg: "700px" }}
        sx={{
          wordWrap: "break-word",
          overflowWrap: "break-word",
          display: isDeletedByUser ? 'none' : 'block', // Hide deleted messages
        }}
      >
        {isDeletedBySender ? (
          <span style={{ color: '#dbdbdb',pointerEvents: 'none', fontStyle: 'italic' }}>Message removed</span>
        ) : (
          message.textBody
        )}
      </Stack>
    </Tooltip>
  );
};

export default ReceivedMessage;
