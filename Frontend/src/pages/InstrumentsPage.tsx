import { Chip, Stack } from "@mui/material";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { useNavigate } from "react-router-dom";
import { PageHeader } from "../components/common/PageHeader";
import { useInstruments } from "../hooks/useInstruments";
import type { Instrument } from "../types/instrument";

const columns: GridColDef<Instrument>[] = [
  {
    field: "deviceId",
    headerName: "Device ID",
    flex: 1,
    minWidth: 160,
  },
  {
    field: "deviceName",
    headerName: "Device Name",
    flex: 1.3,
    minWidth: 180,
  },
  {
    field: "channel",
    headerName: "Channel",
    flex: 0.8,
    minWidth: 120,
  },
  {
    field: "unit",
    headerName: "Unit",
    flex: 0.7,
    minWidth: 120,
    renderCell: (params) => <Chip size="small" label={params.value} />,
  },
  {
    field: "softwareVersion",
    headerName: "Software Version",
    flex: 0.9,
    minWidth: 150,
  },
  {
    field: "instrumentState",
    headerName: "State",
    flex: 0.9,
    minWidth: 140,
    renderCell: (params) => (
      <Chip size="small" color="primary" label={params.value} />
    ),
  },
];

export function InstrumentsPage() {
  const navigate = useNavigate();
  const { data = [], isLoading } = useInstruments();

  return (
    <section className="page-card">
      <PageHeader title="Instruments" />

      <Stack direction="row" spacing={1} sx={{ mb: 2 }}>
        <Chip color="secondary" label={`${data.length} instruments`} />
      </Stack>

      <div className="data-grid-shell">
        <DataGrid
          rows={data}
          columns={columns}
          loading={isLoading}
          getRowId={(row) => row.deviceId}
          onRowClick={(params) =>
            navigate(`/instruments/${params.row.deviceId}`)
          }
          hideFooterPagination
          sx={{
            border: 0,
            "& .MuiDataGrid-row": {
              cursor: "pointer",
            },
          }}
        />
      </div>
    </section>
  );
}
