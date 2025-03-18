"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// Automatically import all TS modules in this folder.
// This syntax makes it non-tree-shaking - which is by design as it's a library
const modules = import.meta.glob('./*.ts', { eager: true });
