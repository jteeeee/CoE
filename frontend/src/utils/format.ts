export const formatDate = (value: string) =>
  new Intl.DateTimeFormat("en-AU", {
    day: "2-digit",
    month: "short",
    year: "numeric"
  }).format(new Date(value));

export const formatDateTime = (value: string) =>
  new Intl.DateTimeFormat("en-AU", {
    day: "2-digit",
    month: "short",
    hour: "2-digit",
    minute: "2-digit"
  }).format(new Date(value));

export const formatNumber = (value: number) => new Intl.NumberFormat("en-AU").format(value);

export const formatPercent = (value: number, fractionDigits = 0) =>
  new Intl.NumberFormat("en-AU", {
    style: "percent",
    maximumFractionDigits: fractionDigits
  }).format(value / 100);

export const formatCapacity = (valueMb: number) => `${Math.round(valueMb / 1024).toLocaleString("en-AU")} GB`;

export const exportRowsAsCsv = (filename: string, rows: Array<Record<string, string | number>>) => {
  const headers = Object.keys(rows[0] ?? {});
  const csv = [
    headers.join(","),
    ...rows.map((row) =>
      headers
        .map((header) => {
          const value = String(row[header] ?? "");
          return `"${value.replace(/"/g, '""')}"`;
        })
        .join(",")
    )
  ].join("\n");

  const blob = new Blob([csv], { type: "text/csv;charset=utf-8" });
  const url = URL.createObjectURL(blob);
  const link = document.createElement("a");
  link.href = url;
  link.download = filename;
  link.click();
  URL.revokeObjectURL(url);
};
