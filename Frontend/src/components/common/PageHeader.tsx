import { Stack, Typography } from "@mui/material";

type PageHeaderProps = {
  title: string;
};

export function PageHeader({ title }: PageHeaderProps) {
  return (
    <Stack spacing={1} sx={{ mb: 3 }}>
      <Typography variant="h3">{title}</Typography>
    </Stack>
  );
}
