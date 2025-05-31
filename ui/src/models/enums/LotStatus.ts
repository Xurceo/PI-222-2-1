export const LotStatus = {
  Pending: 1,
  Confirmed: 2,
  Sold: 3,
  Expired: 4,
  Cancelled: 5,
} as const;

export type LotStatus = (typeof LotStatus)[keyof typeof LotStatus];
