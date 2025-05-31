export function capitalize(word: string): string {
  if (!word) return "";
  return word.charAt(0).toUpperCase() + word.slice(1).toLowerCase();
}
export function cookieExists(name: string): boolean {
  return document.cookie
    .split("; ")
    .some((cookie) => cookie.startsWith(name + "="));
}
