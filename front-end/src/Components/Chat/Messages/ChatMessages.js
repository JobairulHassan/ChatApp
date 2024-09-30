import React, {useState, useContext } from "react";
import { ChatContext } from "../../General/Provider/ChatProvider";
import AuthContext from "../../General/Provider/AuthProvider";
import SendedMessage from "./SendedMessage";
import ReceivedMessage from "./ReceivedMessage";
import { IconButton } from "@mui/material";
import UpdateIcon from "@mui/icons-material/Update";

const ChatMessages = () => {
  const { messages, isThereMoreMessages, loadMessages, handleDeleteMessage } =
    useContext(ChatContext);
  const { user } = useContext(AuthContext);
  const [contextMenu, setContextMenu] = useState({ visible: false, index: null, x: 0, y: 0 });

  const handleContextMenu = (event, index) => {
    event.preventDefault(); // Prevent default context menu
    setContextMenu({
      visible: true,
      index,
      x: event.pageX,
      y: event.pageY,
    });
  };

  const handleCloseMenu = () => {
    setContextMenu({ visible: false, index: null });
  };

  return (
    <>
      {isThereMoreMessages && (
        <IconButton
          type="button"
          sx={{ margin: "auto" }}
          onClick={() => {
            loadMessages(false);
          }}
        >
          <UpdateIcon sx={{ color: "#757575", fontSize: "35px" }} />
        </IconButton>
      )}
      {messages.map((message, index) => {
        var messageType = message.senderId === user.id ? "sended" : "received";
        return (
          <div
            key={message.id}
            onContextMenu={(e) => handleContextMenu(e, index)}
            style={{
              alignSelf: messageType === 'sended' ? 'flex-end' : 'flex-start'
            }}
          >
            {messageType === "sended" ? (
              <SendedMessage message={message} index={index} currentUserId={user.id} />
            ) : (
              <ReceivedMessage message={message} index={index} currentUserId={user.id} />
            )}
          </div>
        );
      })}

      {contextMenu.visible && (
        <div
          style={{
            position: 'absolute',
            top: contextMenu.y,
            left: contextMenu.x,
            backgroundColor: 'white',
            border: '1px solid #ccc',
            zIndex: 1000,
          }}
          onMouseLeave={handleCloseMenu} // Close the menu when mouse leaves
        >
          <button onClick={() => handleDeleteMessage(messages[contextMenu.index].id)}>Delete</button>
        </div>
      )}
    </>
  );
};

export default ChatMessages;
