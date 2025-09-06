import {
  Box,
  AppBar,
  Toolbar,
  Typography,
  Container,
  MenuItem,
} from "@mui/material";
import { Group } from "@mui/icons-material";
import { NavLink } from "react-router";
import MenuItemLink from "../shared/components/MenuItemLink";

export default function NavBar() {
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar
        position="static"
        sx={{
          backgroundColor:
            "linear-gradient(135deg, rgb(28, 27, 27) 0%, rgb(26, 23, 23) 100%)",
        }}
      >
        <Container maxWidth="xl">
          <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
            <Box>
              <MenuItem
                component={NavLink}
                to="/"
                sx={{ display: "flex", gap: 2 }}
              >
                <Group fontSize="large" />
                <Typography variant="h4" fontWeight="bold" color="white">
                  Reactivities
                </Typography> 
              </MenuItem>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
              <MenuItemLink to="/activities">Activities</MenuItemLink>
              <MenuItemLink to="/createActivity">Create Activity</MenuItemLink>
            </Box>
            <MenuItem>User Menu</MenuItem>
          </Toolbar>
        </Container>
      </AppBar>
    </Box>
  );
}
