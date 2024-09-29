import { createTheme } from "@mui/material/styles";

const lightTheme = createTheme({
  palette: {
    mode: "light",
    primary: {
      main: "#4287f5",
      light: "#4287f5c2",
    },
    secondary: {
      main: "#f50057",
    },
    background: {
      paper: "#f7f7f7",
    },
    text: {
      primary: "#3d3d3d",
    },
    input: {
      primary: "white",
    },
  },
});

const darkTheme = createTheme({
  palette: {
    mode: "dark",
    primary: {
      main: "#4287f5",
      light: "#4287f5c2",
    },
    secondary: {
      main: "#f48fb1",
    },
    background: {
      default: "#333333",
      paper: "#232323",
    },
    text: {
      primary: "#f0eded",
    },
    input: {
      primary: "#232323",
    },
  },
});

export { lightTheme, darkTheme };
