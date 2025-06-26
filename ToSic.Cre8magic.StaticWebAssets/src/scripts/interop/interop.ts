import { GoogleTagManager } from './gtm';

// Re-export all interop functions from their respective modules
export * from './body-classes';
export * from './gtm';

export const gtm = new GoogleTagManager();