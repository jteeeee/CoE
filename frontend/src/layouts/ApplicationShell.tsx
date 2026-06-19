import type { ReactNode } from "react";
import { useState } from "react";
import {
  AccountTree,
  AdminPanelSettings,
  Apps,
  AltRoute,
  ChevronLeft,
  ChevronRight,
  Dashboard,
  DarkMode,
  LightMode,
  Menu,
  NotificationsNone,
  Settings
} from "@mui/icons-material";
import {
  AppBar,
  Avatar,
  Box,
  Divider,
  Drawer,
  IconButton,
  List,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Stack,
  Toolbar,
  Tooltip,
  Typography,
  useMediaQuery
} from "@mui/material";
import { alpha, useTheme } from "@mui/material/styles";
import type { PaletteMode } from "@mui/material";
import { NavLink, useLocation } from "react-router-dom";

const expandedWidth = 264;
const collapsedWidth = 76;
const headerHeight = 64;

const navigationItems = [
  { label: "Dashboard", path: "/dashboard", icon: <Dashboard /> },
  { label: "Applications", path: "/applications", icon: <Apps /> },
  { label: "Flows", path: "/flows", icon: <AltRoute /> },
  { label: "Environments", path: "/environments", icon: <AccountTree /> },
  { label: "Governance", path: "/governance", icon: <AdminPanelSettings />, disabled: true },
  { label: "Administration", path: "/administration", icon: <Settings />, disabled: true }
];

interface ApplicationShellProps {
  children: ReactNode;
  mode: PaletteMode;
  onToggleMode: () => void;
}

export function ApplicationShell({ children, mode, onToggleMode }: ApplicationShellProps) {
  const theme = useTheme();
  const location = useLocation();
  const isTablet = useMediaQuery(theme.breakpoints.down("md"));
  const [open, setOpen] = useState(true);
  const drawerWidth = open && !isTablet ? expandedWidth : collapsedWidth;

  return (
    <Box sx={{ display: "flex", minHeight: "100vh", bgcolor: "background.default" }}>
      <AppBar
        position="fixed"
        color="inherit"
        elevation={0}
        sx={{
          height: headerHeight,
          borderBottom: 1,
          borderColor: "divider",
          zIndex: theme.zIndex.drawer + 1
        }}
      >
        <Toolbar sx={{ minHeight: `${headerHeight}px !important`, px: 3 }}>
          <Stack direction="row" spacing={1.5} alignItems="center" sx={{ minWidth: 0, flex: 1 }}>
            <IconButton aria-label={open ? "Collapse navigation" : "Expand navigation"} onClick={() => setOpen((value) => !value)}>
              <Menu />
            </IconButton>
            <Box>
              <Typography variant="h3" noWrap>
                Power Platform Governance
              </Typography>
              <Typography variant="caption" color="text.secondary" noWrap>
                Enterprise administration and operational intelligence
              </Typography>
            </Box>
          </Stack>

          <Stack direction="row" spacing={1} alignItems="center">
            <Tooltip title="Notifications">
              <IconButton aria-label="Notifications">
                <NotificationsNone />
              </IconButton>
            </Tooltip>
            <Tooltip title={mode === "light" ? "Switch to dark mode" : "Switch to light mode"}>
              <IconButton aria-label="Toggle colour mode" onClick={onToggleMode}>
                {mode === "light" ? <DarkMode /> : <LightMode />}
              </IconButton>
            </Tooltip>
            <Divider orientation="vertical" flexItem sx={{ mx: 0.5 }} />
            <Stack direction="row" spacing={1} alignItems="center">
              <Avatar sx={{ width: 32, height: 32, bgcolor: "primary.main", fontSize: 14 }}>PA</Avatar>
              <Box sx={{ display: { xs: "none", md: "block" } }}>
                <Typography variant="body2" fontWeight={700} noWrap>
                  Platform Admin
                </Typography>
                <Typography variant="caption" color="text.secondary" noWrap>
                  Governance Operations
                </Typography>
              </Box>
            </Stack>
          </Stack>
        </Toolbar>
      </AppBar>

      <Drawer
        variant="permanent"
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          "& .MuiDrawer-paper": {
            width: drawerWidth,
            overflowX: "hidden",
            borderRight: 1,
            borderColor: "divider",
            transition: theme.transitions.create("width", {
              duration: theme.transitions.duration.shorter
            })
          }
        }}
      >
        <Toolbar sx={{ minHeight: `${headerHeight}px !important` }} />
        <Box sx={{ p: 1.5 }}>
          <List component="nav" aria-label="Main navigation">
            {navigationItems.map((item) => {
              const selected = location.pathname === item.path;

              return (
                <Tooltip key={item.label} title={!open || isTablet ? item.label : ""} placement="right">
                  <ListItemButton
                    component={item.disabled ? "button" : NavLink}
                    to={item.disabled ? undefined : item.path}
                    disabled={item.disabled}
                    selected={selected}
                    sx={{
                      mb: 0.5,
                      minHeight: 44,
                      borderRadius: 2,
                      justifyContent: open && !isTablet ? "initial" : "center",
                      "&.Mui-selected": {
                        bgcolor: alpha(theme.palette.primary.main, 0.1),
                        color: "primary.main"
                      }
                    }}
                  >
                    <ListItemIcon
                      sx={{
                        minWidth: 0,
                        mr: open && !isTablet ? 1.5 : 0,
                        justifyContent: "center",
                        color: selected ? "primary.main" : "text.secondary"
                      }}
                    >
                      {item.icon}
                    </ListItemIcon>
                    {open && !isTablet ? <ListItemText primary={item.label} /> : null}
                  </ListItemButton>
                </Tooltip>
              );
            })}
          </List>
        </Box>
        <Box sx={{ flex: 1 }} />
        <Box sx={{ p: 1.5 }}>
          <Tooltip title={open && !isTablet ? "Collapse navigation" : "Expand navigation"} placement="right">
            <IconButton
              aria-label={open && !isTablet ? "Collapse navigation" : "Expand navigation"}
              onClick={() => setOpen((value) => !value)}
              sx={{ width: "100%" }}
            >
              {open && !isTablet ? <ChevronLeft /> : <ChevronRight />}
            </IconButton>
          </Tooltip>
        </Box>
      </Drawer>

      <Box component="main" sx={{ flex: 1, minWidth: 0, pt: `${headerHeight}px` }}>
        <Box sx={{ p: { xs: 2, md: 3 } }}>{children}</Box>
      </Box>
    </Box>
  );
}
