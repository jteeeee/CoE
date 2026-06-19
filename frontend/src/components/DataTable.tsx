import type { ReactNode } from "react";
import { useEffect, useMemo, useState } from "react";
import {
  Check,
  DeleteOutline,
  EditOutlined,
  FileDownloadOutlined,
  MoreVert,
  Refresh,
  Search,
  Tune,
  VisibilityOutlined
} from "@mui/icons-material";
import {
  Box,
  Card,
  CardContent,
  Checkbox,
  Divider,
  IconButton,
  InputAdornment,
  Menu,
  MenuItem,
  Select,
  Skeleton,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TablePagination,
  TableRow,
  TableSortLabel,
  TextField,
  Tooltip,
  Typography
} from "@mui/material";
import { EmptyState } from "./EmptyState";
import { ErrorState } from "./ErrorState";

type Order = "asc" | "desc";

export interface DataTableColumn<T> {
  id: string;
  label: string;
  align?: "left" | "right" | "center";
  minWidth?: number;
  format?: (row: T) => ReactNode;
  sortValue?: (row: T) => string | number;
  hideable?: boolean;
}

interface DataTableFilter {
  label: string;
  value: string;
  options: { label: string; value: string }[];
  onChange: (value: string) => void;
}

interface DataTableProps<T> {
  title: string;
  rows: T[];
  columns: DataTableColumn<T>[];
  getRowId: (row: T) => string;
  searchValue: string;
  onSearchChange: (value: string) => void;
  searchPlaceholder: string;
  filters?: DataTableFilter[];
  loading?: boolean;
  error?: boolean;
  emptyTitle: string;
  emptyDescription: string;
  onRefresh?: () => void;
  onExport?: () => void;
}

function RowActions() {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

  return (
    <>
      <Tooltip title="Row actions">
        <IconButton size="small" aria-label="Row actions" onClick={(event) => setAnchorEl(event.currentTarget)}>
          <MoreVert fontSize="small" />
        </IconButton>
      </Tooltip>
      <Menu anchorEl={anchorEl} open={Boolean(anchorEl)} onClose={() => setAnchorEl(null)}>
        <MenuItem onClick={() => setAnchorEl(null)}>
          <VisibilityOutlined fontSize="small" sx={{ mr: 1 }} />
          View
        </MenuItem>
        <MenuItem onClick={() => setAnchorEl(null)}>
          <EditOutlined fontSize="small" sx={{ mr: 1 }} />
          Edit governance
        </MenuItem>
        <MenuItem disabled>
          <DeleteOutline fontSize="small" sx={{ mr: 1 }} />
          Delete
        </MenuItem>
      </Menu>
    </>
  );
}

export function DataTable<T>({
  title,
  rows,
  columns,
  getRowId,
  searchValue,
  onSearchChange,
  searchPlaceholder,
  filters = [],
  loading = false,
  error = false,
  emptyTitle,
  emptyDescription,
  onRefresh,
  onExport
}: DataTableProps<T>) {
  const [order, setOrder] = useState<Order>("asc");
  const [orderBy, setOrderBy] = useState(columns[0]?.id ?? "");
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [columnAnchorEl, setColumnAnchorEl] = useState<null | HTMLElement>(null);
  const [visibleColumns, setVisibleColumns] = useState(() => columns.map((column) => column.id));
  const [searchInput, setSearchInput] = useState(searchValue);

  useEffect(() => {
    setSearchInput(searchValue);
  }, [searchValue]);

  useEffect(() => {
    const handle = window.setTimeout(() => {
      if (searchInput !== searchValue) {
        setPage(0);
        onSearchChange(searchInput);
      }
    }, 300);

    return () => window.clearTimeout(handle);
  }, [onSearchChange, searchInput, searchValue]);

  const sortedRows = useMemo(() => {
    const activeColumn = columns.find((column) => column.id === orderBy);
    const direction = order === "asc" ? 1 : -1;

    return [...rows].sort((a, b) => {
      const left = activeColumn?.sortValue?.(a) ?? "";
      const right = activeColumn?.sortValue?.(b) ?? "";

      if (left < right) {
        return -1 * direction;
      }

      if (left > right) {
        return 1 * direction;
      }

      return 0;
    });
  }, [columns, order, orderBy, rows]);

  const visibleTableColumns = columns.filter((column) => visibleColumns.includes(column.id));
  const pagedRows = sortedRows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage);

  const handleSort = (columnId: string) => {
    const isCurrentColumn = orderBy === columnId;
    setOrder(isCurrentColumn && order === "asc" ? "desc" : "asc");
    setOrderBy(columnId);
  };

  const toggleColumn = (columnId: string) => {
    setVisibleColumns((current) =>
      current.includes(columnId) ? current.filter((id) => id !== columnId) : [...current, columnId]
    );
  };

  return (
    <Card>
      <CardContent sx={{ pb: 0 }}>
        <Stack spacing={2}>
          <Stack direction={{ xs: "column", lg: "row" }} spacing={2} justifyContent="space-between" alignItems={{ xs: "stretch", lg: "center" }}>
            <Typography variant="h2">{title}</Typography>
            <Stack direction={{ xs: "column", md: "row" }} spacing={1.5} alignItems={{ xs: "stretch", md: "center" }}>
              <TextField
                size="small"
                value={searchInput}
                onChange={(event) => {
                  setSearchInput(event.target.value);
                }}
                placeholder={searchPlaceholder}
                aria-label={searchPlaceholder}
                InputProps={{
                  startAdornment: (
                    <InputAdornment position="start">
                      <Search fontSize="small" />
                    </InputAdornment>
                  )
                }}
                sx={{ minWidth: { xs: "100%", md: 320 } }}
              />
              {filters.map((filter) => (
                <Select
                  key={filter.label}
                  size="small"
                  value={filter.value}
                  displayEmpty
                  aria-label={filter.label}
                  onChange={(event) => {
                    setPage(0);
                    filter.onChange(event.target.value);
                  }}
                  sx={{ minWidth: 180 }}
                >
                  {filter.options.map((option) => (
                    <MenuItem key={option.value} value={option.value}>
                      {option.label}
                    </MenuItem>
                  ))}
                </Select>
              ))}
              <Stack direction="row" spacing={0.5}>
                <Tooltip title="Refresh">
                  <span>
                    <IconButton aria-label="Refresh table" onClick={onRefresh} disabled={!onRefresh}>
                      <Refresh />
                    </IconButton>
                  </span>
                </Tooltip>
                <Tooltip title="Export">
                  <span>
                    <IconButton aria-label="Export table" onClick={onExport} disabled={!onExport}>
                      <FileDownloadOutlined />
                    </IconButton>
                  </span>
                </Tooltip>
                <Tooltip title="Column settings">
                  <IconButton aria-label="Column settings" onClick={(event) => setColumnAnchorEl(event.currentTarget)}>
                    <Tune />
                  </IconButton>
                </Tooltip>
              </Stack>
            </Stack>
          </Stack>
          <Divider />
        </Stack>
      </CardContent>

      <Menu anchorEl={columnAnchorEl} open={Boolean(columnAnchorEl)} onClose={() => setColumnAnchorEl(null)}>
        {columns.map((column) => {
          const checked = visibleColumns.includes(column.id);
          const disabled = column.hideable === false || (checked && visibleColumns.length === 1);

          return (
            <MenuItem key={column.id} disabled={disabled} onClick={() => toggleColumn(column.id)}>
              <Checkbox size="small" checked={checked} disabled={disabled} />
              {column.label}
              {checked ? <Check fontSize="small" sx={{ ml: "auto" }} /> : null}
            </MenuItem>
          );
        })}
      </Menu>

      {error ? (
        <Box sx={{ p: 3 }}>
          <ErrorState />
        </Box>
      ) : loading ? (
        <Stack spacing={1} sx={{ p: 3 }}>
          {Array.from({ length: 8 }).map((_, index) => (
            <Skeleton key={index} variant="rounded" height={44} />
          ))}
        </Stack>
      ) : sortedRows.length === 0 ? (
        <EmptyState title={emptyTitle} description={emptyDescription} />
      ) : (
        <>
          <TableContainer>
            <Table stickyHeader size="medium" aria-label={title}>
              <TableHead>
                <TableRow>
                  {visibleTableColumns.map((column) => (
                    <TableCell key={column.id} align={column.align ?? "left"} sx={{ minWidth: column.minWidth }}>
                      <TableSortLabel active={orderBy === column.id} direction={orderBy === column.id ? order : "asc"} onClick={() => handleSort(column.id)}>
                        {column.label}
                      </TableSortLabel>
                    </TableCell>
                  ))}
                  <TableCell align="right" sx={{ width: 72 }}>
                    Actions
                  </TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {pagedRows.map((row) => (
                  <TableRow hover key={getRowId(row)}>
                    {visibleTableColumns.map((column) => (
                      <TableCell key={column.id} align={column.align ?? "left"}>
                        {column.format ? column.format(row) : null}
                      </TableCell>
                    ))}
                    <TableCell align="right">
                      <RowActions />
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
          <TablePagination
            component="div"
            count={sortedRows.length}
            page={page}
            onPageChange={(_, nextPage) => setPage(nextPage)}
            rowsPerPage={rowsPerPage}
            rowsPerPageOptions={[5, 10, 25, 50]}
            onRowsPerPageChange={(event) => {
              setRowsPerPage(Number(event.target.value));
              setPage(0);
            }}
          />
        </>
      )}
    </Card>
  );
}
