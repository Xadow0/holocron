/// <reference types="vite/client" />

interface ImportMetaEnv {
    VITE_BACKEND_BASE_URL: string;
    // Add other environment variables here
}

interface ImportMeta {
    readonly env: ImportMetaEnv;
}
