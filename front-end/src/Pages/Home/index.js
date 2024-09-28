import React, { useContext } from "react";
import LayoutContainer from "../../Components/General/ThreeColumnLayout/LayoutContainer";
import LeftItem from "../../Components/General/ThreeColumnLayout/LeftItem";
import MiddleItem from "../../Components/General/ThreeColumnLayout/MiddleItem";
import LeftSidebar from "../../Components/LeftSidebar";
import Background from "../../Components/General/Background";
import AuthContext from "../../Components/General/Provider/AuthProvider";
import ChatContextProvider from "../../Components/General/Provider/ChatProvider";
import Chat from "../../Components/Chat";

const Home = () => {
  const { user } = useContext(AuthContext);
  return user === null ? (
    <Background />
  ) : (
    <ChatContextProvider>
      <LayoutContainer>
        <LeftItem>
          <LeftSidebar />
        </LeftItem>
        <MiddleItem>
          <Chat />
        </MiddleItem>
      </LayoutContainer>
    </ChatContextProvider>
  );
};

export default Home;
