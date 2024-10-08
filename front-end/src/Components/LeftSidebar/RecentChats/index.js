import React, { useContext, useEffect, useState } from "react";
import AuthContext from "../../General/Provider/AuthProvider";
import axios from "axios";
import {
  Box,
  CircularProgress,
  List,
  ListItem,
  ListItemButton,
} from "@mui/material";
import { ChatContext } from "../../General/Provider/ChatProvider";
import ChatCard from "./ChatCard";

const RecentChats = ({ displayState }) => {
  const [isLoading, setIsLoading] = useState(false);
  const [chats, setChats] = useState([]);
  const { token, user } = useContext(AuthContext);
  const { selectedUser, setSelectedUser, newMessage } = useContext(ChatContext);

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true);
      await axios
        .get(`http://localhost:7271/api/users/${user.id}/recent-chats`, {
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
            Authorization: `bearer ${token}`,
          },
        })
        .then((response) => {
          setChats(response.data);
        });
      setIsLoading(false);
    };
    fetchData();
  }, [token, user]);

  useEffect(() => {
    const fetchData = async () => {
      const { senderId, receiverId } = newMessage;
      const chatUserId = senderId === user.id ? receiverId : senderId;
      const chatIndex = chats.findIndex((chat) => chat.user.id === chatUserId);
      const updatedChats = [...chats];
      if (chatIndex !== -1) {
        const newChat = {
          ...updatedChats[chatIndex],
          lastMessage: newMessage,
        };
        updatedChats.splice(chatIndex, 1);
        updatedChats.unshift(newChat);
      } else {
        const chatUser = await getUserById(chatUserId);
        const newChat = { user: chatUser, lastMessage: newMessage };
        updatedChats.unshift(newChat);
      }
      setChats(updatedChats);
    };
    if (newMessage) {
      fetchData();
    }
  }, [newMessage, user.id]);

  const getUserById = async (userId) => {
    var newUser;
    await axios
      .get(`http://localhost:7271/api/users/${userId}`, {
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: `bearer ${token}`,
        },
      })
      .then((response) => {
        newUser = response.data;
      });

    return newUser;
  };

  return isLoading ? (
    <Box flex={4}>
      <CircularProgress color="inherit" size={16} />
    </Box>
  ) : (
    <List
      sx={{
        "&& .Mui-selected": {
          backgroundColor: "#75757520",
        },
        display: displayState ? "none" : "block",
      }}
    >
      {chats.map((chat, index) => {
        return (
          <ListItem disablePadding key={index}>
            <ListItemButton
              selected={selectedUser?.id === chat.user.id} // Move selected here
              onClick={() => setSelectedUser(chat.user)}
            >
              <ChatCard chat={chat} />
            </ListItemButton>
          </ListItem>
        );
      })}
    </List>
  );
};

export default RecentChats;
