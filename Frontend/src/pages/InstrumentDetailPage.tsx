import ArrowBackRoundedIcon from "@mui/icons-material/ArrowBackRounded";
import PlayArrowRoundedIcon from "@mui/icons-material/PlayArrowRounded";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
  Alert,
  Button,
  CircularProgress,
  Grid,
  Paper,
  Snackbar,
  Stack,
  Typography,
} from "@mui/material";
import { Link as RouterLink, useParams } from "react-router-dom";
import { useState } from "react";
import { InstrumentSummary } from "../components/instrument/Instrument";
import { runSelfTest, startMeasurement } from "../api/instruments";
import { useInstrument } from "../hooks/useInstruments";
import type { Instrument } from "../types/instrument";

export function InstrumentDetailPage() {
  const { deviceId = "" } = useParams();
  const queryClient = useQueryClient();
  const { data, isLoading, isError } = useInstrument(deviceId);
  const [feedbackOpen, setFeedbackOpen] = useState(false);
  const [feedbackSeverity, setFeedbackSeverity] = useState<"success" | "error">(
    "success",
  );
  const [feedbackMessage, setFeedbackMessage] = useState("");

  const startMeasurementMutation = useMutation({
    mutationFn: (selectedDeviceId: string) => startMeasurement(selectedDeviceId),
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

  const selfTestMutation = useMutation({
    mutationFn: (selectedDeviceId: string) => runSelfTest(selectedDeviceId),
    onSuccess: (updatedInstrument) => {
      queryClient.setQueryData(["instruments", updatedInstrument.deviceId], updatedInstrument);
      queryClient.setQueryData(["instruments"], (current: Instrument[] | undefined) =>
        current?.map((instrument) =>
          instrument.deviceId === updatedInstrument.deviceId
            ? updatedInstrument
            : instrument,
        ) ?? current,
      );
      setFeedbackSeverity("success");
      setFeedbackMessage("Self test completed successfully.");
      setFeedbackOpen(true);
    },
    onError: () => {
      setFeedbackSeverity("error");
      setFeedbackMessage("Self test failed.");
      setFeedbackOpen(true);
    },
  });

  if (isLoading) {
    return (
      <section className="page-card">
        <Stack
          alignItems="center"
          justifyContent="center"
          sx={{ minHeight: 320 }}
        >
          <CircularProgress />
        </Stack>
      </section>
    );
  }

  if (isError || !data) {
    return (
      <section className="page-card">
        <Alert severity="error">
          The selected instrument could not be found.
        </Alert>
      </section>
    );
  }

  return (
    <section className="page-card">
      <Stack spacing={3}>
        <Stack
          direction="row"
          justifyContent="space-between"
          alignItems="center"
        >
          <Button
            component={RouterLink}
            to="/instruments"
            startIcon={<ArrowBackRoundedIcon />}
          >
            Back to Instruments
          </Button>
          <Stack direction="row" spacing={1.5}>
            <Button
              variant="outlined"
              startIcon={<PlayArrowRoundedIcon />}
              onClick={() => selfTestMutation.mutate(data.deviceId)}
              disabled={selfTestMutation.isPending}
            >
              {selfTestMutation.isPending ? "Running Self Test..." : "Self Test"}
            </Button>
            <Button
              variant="contained"
              startIcon={<PlayArrowRoundedIcon />}
              onClick={() => startMeasurementMutation.mutate(data.deviceId)}
              disabled={startMeasurementMutation.isPending}
            >
              {startMeasurementMutation.isPending
                ? "Starting..."
                : "Start Measurement"}
            </Button>
          </Stack>
        </Stack>

        <InstrumentSummary instrument={data} />

        <Grid container spacing={2}>
          <Grid size={{ xs: 12, md: 3 }}>
            <Paper sx={{ p: 3, height: "100%" }}>
              <Typography variant="overline" color="primary">
                Device ID
              </Typography>
              <Typography variant="h6">{data.deviceId}</Typography>
            </Paper>
          </Grid>
          <Grid size={{ xs: 12, md: 3 }}>
            <Paper sx={{ p: 3, height: "100%" }}>
              <Typography variant="overline" color="primary">
                Channel
              </Typography>
              <Typography variant="h6">{data.channel}</Typography>
            </Paper>
          </Grid>
          <Grid size={{ xs: 12, md: 3 }}>
            <Paper sx={{ p: 3, height: "100%" }}>
              <Typography variant="overline" color="primary">
                Unit
              </Typography>
              <Typography variant="h6">{data.unit}</Typography>
            </Paper>
          </Grid>
          <Grid size={{ xs: 12, md: 3 }}>
            <Paper sx={{ p: 3, height: "100%" }}>
              <Typography variant="overline" color="primary">
                Instrument State
              </Typography>
              <Typography variant="h6">{data.instrumentState}</Typography>
            </Paper>
          </Grid>
        </Grid>
      </Stack>

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
    </section>
  );
}
