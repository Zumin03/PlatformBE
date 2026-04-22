import { Chip, Stack } from "@mui/material";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { useNavigate } from "react-router-dom";
import { PageHeader } from "../components/common/PageHeader";
import { useMeasurements } from "../hooks/useMeasurements";
import type { Measurement } from "../types/measurement";

const columns: GridColDef<Measurement>[] = [
  {
    field: "deviceName",
    headerName: "Device",
    flex: 1.2,
    minWidth: 180,
  },
  {
    field: "channel",
    headerName: "Channel",
    flex: 0.8,
    minWidth: 120,
  },
  {
    field: "value",
    headerName: "Value",
    type: "number",
    flex: 0.7,
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
    field: "measuredAt",
    headerName: "Measured At",
    flex: 1,
    minWidth: 220,
    valueFormatter: (value) =>
      new Intl.DateTimeFormat("en-EN", {
        year: "numeric",
        month: "2-digit",
        day: "2-digit",
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit",
      }).format(new Date(value)),
  },
];

export function MeasurementsPage() {
  const navigate = useNavigate();
  const { data = [], isLoading } = useMeasurements();

  return (
    <section className="page-card">
      <PageHeader title="Measurements" />

      <Stack direction="row" spacing={1} sx={{ mb: 2 }}>
        <Chip color="primary" label={`${data.length} measurements`} />
      </Stack>

      <div className="data-grid-wrapper">
        <DataGrid
          rows={data}
          columns={columns}
          loading={isLoading}
          getRowId={(row) =>
            `${row.deviceName}-${row.channel}-${row.measuredAt}`
          }
          onRowClick={(params) =>
            navigate(`/instruments/${params.row.deviceId}`)
          }
          disableRowSelectionOnClick
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
