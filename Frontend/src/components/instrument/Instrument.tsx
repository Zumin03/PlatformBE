import { Chip, Stack, Typography } from "@mui/material";
import type { Instrument } from "../../types/instrument";

type InstrumentSummaryProps = {
  instrument: Instrument;
};

export function InstrumentSummary({ instrument }: InstrumentSummaryProps) {
  return (
    <Stack spacing={2}>
      <Typography variant="h4">{instrument.deviceName}</Typography>
      <Stack direction="row" spacing={1} useFlexGap flexWrap="wrap">
        <Chip label={`ID: ${instrument.deviceId}`} />
        <Chip label={`Channel: ${instrument.channel}`} />
        <Chip label={`Unit: ${instrument.unit}`} />
        <Chip label={`Version: ${instrument.softwareVersion}`} />
        <Chip color="primary" label={`State: ${instrument.instrumentState}`} />
      </Stack>
    </Stack>
  );
}
