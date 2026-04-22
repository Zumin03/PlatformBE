import SpeedRoundedIcon from "@mui/icons-material/SpeedRounded";
import MemoryRoundedIcon from "@mui/icons-material/MemoryRounded";
import MenuRoundedIcon from "@mui/icons-material/MenuRounded";
import PlayArrowRoundedIcon from "@mui/icons-material/PlayArrowRounded";
import {
  Alert,
  AppBar,
  Autocomplete,
  Box,
  Button,
  Drawer,
  IconButton,
  List,
  ListItemButton,
  Paper,
  Snackbar,
  Stack,
  TextField,
  Toolbar,
  Typography,
} from "@mui/material";
import { useState } from "react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { NavLink, Outlet, useLocation } from "react-router-dom";
import "../../App.css";
import { startMeasurement } from "../../api/instruments";
import { useInstruments } from "../../hooks/useInstruments";
import type { Instrument } from "../../types/instrument";

const FOOTER_HEIGHT = 100;

const navigationItems = [
  {
    label: "Measurements",
    to: "/measurements",
    icon: <SpeedRoundedIcon />,
  },
  {
    label: "Instruments",
    to: "/instruments",
    icon: <MemoryRoundedIcon />,
  },
];

export function AppLayout() {
  const location = useLocation();
  const queryClient = useQueryClient();
  const { data: instruments = [] } = useInstruments();
  const [mobileOpen, setMobileOpen] = useState(false);
  const [selectedInstrument, setSelectedInstrument] =
    useState<Instrument | null>(null);
  const [feedbackOpen, setFeedbackOpen] = useState(false);
  const [feedbackSeverity, setFeedbackSeverity] = useState<"success" | "error">(
    "success",
  );
  const [feedbackMessage, setFeedbackMessage] = useState("");

  const startMeasurementMutation = useMutation({
    mutationFn: (deviceId: string) => startMeasurement(deviceId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["measurements"] });
      setFeedbackSeverity("success");
      setFeedbackMessage("Measurement completed successfully.");
      setFeedbackOpen(true);
    },
    onError: () => {
      setFeedbackSeverity("error");
      setFeedbackMessage("Failed to start measurement.");
      setFeedbackOpen(true);
    },
  });

  const handleStartMeasurement = () => {
    if (!selectedInstrument) {
      return;
    }

    startMeasurementMutation.mutate(selectedInstrument.deviceId);
  };

  const navigation = (
    <List
      sx={{
        p: { xs: 2, md: 0 },
        display: { xs: "block", md: "flex" },
        alignItems: "center",
      }}
    >
      {navigationItems.map((item) => {
        const selected = location.pathname.startsWith(item.to);

        return (
          <ListItemButton
            key={item.to}
            component={NavLink}
            onClick={() => setMobileOpen(false)}
            to={item.to}
            sx={{
              borderRadius: 1.5,
              mr: { md: 1 },
              mb: { xs: 1, md: 0 },
              color: "inherit",
              backgroundColor: selected ? "action.selected" : "transparent",
              width: { xs: "100%", md: "auto" },
              px: 2,
              flex: { md: "0 0 auto" },
            }}
          >
            <Stack direction="row" spacing={1} alignItems="center">
              {item.icon}
              <Typography variant="button" sx={{ textTransform: "none" }}>
                {item.label}
              </Typography>
            </Stack>
          </ListItemButton>
        );
      })}
    </List>
  );

  return (
    <Box className="app-wrapper">
      <AppBar position="fixed" elevation={0}>
        <Toolbar>
          <IconButton
            color="inherit"
            edge="start"
            onClick={() => setMobileOpen(true)}
            sx={{ display: { xs: "inline-flex", md: "none" }, mr: 1 }}
          >
            <MenuRoundedIcon />
          </IconButton>
          <Box sx={{ mr: 4 }}>
            <Typography
              variant="overline"
              sx={{ display: "block", lineHeight: 1.2 }}
            >
              Measurement Platform UI
            </Typography>
          </Box>
          <Box sx={{ display: { xs: "none", md: "block" }, flexGrow: 1 }}>
            {navigation}
          </Box>
        </Toolbar>
      </AppBar>

      <Drawer
        open={mobileOpen}
        onClose={() => setMobileOpen(false)}
        sx={{
          display: { xs: "block", md: "none" },
          "& .MuiDrawer-paper": { width: 280 },
        }}
      >
        <Box sx={{ p: 2 }}>
          <Stack
            direction="row"
            spacing={1.5}
            alignItems="center"
            sx={{ px: 1, pb: 2 }}
          >
            <Box>
              <Typography variant="overline" color="text.secondary">
                Measurement Platform UI
              </Typography>
            </Box>
          </Stack>
          {navigation}
        </Box>
      </Drawer>

      <Box className="content-frame">
        <Outlet />
      </Box>

      <Paper
        square
        elevation={12}
        sx={{
          position: "fixed",
          left: 0,
          right: 0,
          bottom: 0,
          height: FOOTER_HEIGHT,
          px: { xs: 2, md: 3 },
          py: 2,
          borderTop: 1,
          borderColor: "divider",
          backgroundColor: "background.paper",
          zIndex: 1250,
        }}
      >
        <Stack
          direction={{ xs: "column", md: "row" }}
          spacing={2}
          alignItems={{ xs: "stretch", md: "center" }}
          justifyContent="space-between"
          sx={{ height: "100%" }}
        >
          <Box>
            <Typography variant="overline">Start Measurement</Typography>
          </Box>

          <Stack
            direction={{ xs: "column", sm: "row" }}
            spacing={1.5}
            sx={{ width: { xs: "100%", md: "auto" } }}
          >
            <Autocomplete
              options={instruments}
              getOptionLabel={(option) =>
                `${option.deviceName} (${option.unit})`
              }
              onChange={(_, value) => setSelectedInstrument(value)}
              sx={{ minWidth: { xs: "100%", sm: 360 } }}
              renderInput={(params) => (
                <TextField
                  {...params}
                  label="Instrument"
                  placeholder="Select an instrument"
                />
              )}
            />
            <Button
              variant="contained"
              startIcon={<PlayArrowRoundedIcon />}
              disabled={
                !selectedInstrument || startMeasurementMutation.isPending
              }
              onClick={handleStartMeasurement}
              sx={{ minWidth: 140 }}
            >
              {startMeasurementMutation.isPending ? "Starting..." : "Start"}
            </Button>
          </Stack>
        </Stack>
      </Paper>

      <Snackbar
        open={feedbackOpen}
        autoHideDuration={3000}
        onClose={() => setFeedbackOpen(false)}
        anchorOrigin={{ vertical: "top", horizontal: "right" }}
      >
        <Alert
          severity={feedbackSeverity}
          onClose={() => setFeedbackOpen(false)}
        >
          {feedbackMessage}
        </Alert>
      </Snackbar>
    </Box>
  );
}
