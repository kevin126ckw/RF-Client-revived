# RF-Client API Documentation

## 1. Overview
This documentation describes the API server implementation for the RF-Client game client. The API is RESTful, using JSON for data exchange (except for file uploads which use `multipart/form-data`).

**Base URL:** `https://ra2yr.dreamcloud.top:9999/`

## 2. Response Structure
All API responses are wrapped in a generic `ResResult` object:
```json
{
  "code": "200",       // Status code (String)
  "message": "Success", // Status message
  "data": { ... }       // Actual payload
}
```

## 3. Authentication
The API uses **Bearer Token** authentication.
1.  **Login**: POST `/user/login` with `{name, pwd}`.
2.  **Response**: Returns a `TokenVo` containing `access_token` and `user` profile.
3.  **Usage**: Add header `Authorization: Bearer <access_token>` to all protected requests.

## 4. Key Workflows

### 4.1 User Registration
1.  **Get Verification Code**: Call `GET /user/getSignCode?email=user@example.com`.
2.  **Register**: Call `POST /user/register` with the `User` object (including `username`, `password`, `email`, `side`). The verification code is expected to be validated server-side (though the client sends it in the `User` object or separately - *Note: The client code shows validation happens locally before sending the register request, but the server likely validates the email/code association state.*)

### 4.2 Workshop (Components)
*   **List**: `GET /component/getAllComponent?id=<userId>` retrieves user's uploads.
*   **Upload**: `POST /component/addComponent` as `multipart/form-data`.
    *   Fields: `name`, `author`, `description`, `tags`, `type`, `version`, `uploadUser`.
    *   File: `file` (The 7z archive).
*   **Update**: `POST /component/updComponent` similar to upload but requires `id`.

### 4.3 Update System
The client checks for updates in two steps:
1.  **Get Servers**: `GET /updaterServer/getAllUpdaterServer` to find available mirrors.
2.  **Check Version**: `GET /updater/getNewLatestInfoByBaseVersion?type=<channel>&baseVersion=<current_ver>`.
    *   Returns `UpdaterInfo` containing the new version number, download URL (or file path relative to server), hash, and size.

### 4.4 Creator Certification (Question Bank)
Users must pass a quiz to become creators.
*   **Get Questions**: `GET /questionBank/getQuestionBankByUserID?id=<userId>`.
*   **Submit/Edit**: `POST /questionBank/addQuestionBank` or `updQuestionBank`.

### 4.5 Online Status & Ban Check
The client performs checks when entering the online lobby:
1.  **Check Ban**: `GET /user/checkBanUserByMac?mac=<mac_address>`.
    *   Returns a string message (e.g., expiration date) if banned, or `null` if allowed.
2.  **Report Online**: `POST /user/addOnlineUser`.
    *   Body: `{ "username": "PlayerName", "mac": "MachineCode" }`.
    *   Registers the user session on the server.

## 5. Data Models
*   **User**: Represents a player profile. Contains `id`, `username`, `side` (faction), `badge`, `exp`.
*   **Component**: Represents a workshop item (Map, Mission, Mod).
*   **MissionPack**: Represents a collection of missions (managed via INI files locally, but API interaction is via Component).
*   **Badge**: Achievements/Rank icons.

## 6. System Configuration
The client uses `/dict/getValue` to retrieve dynamic configuration:
*   `section=user&key=side`: List of game factions.
*   `section=component&key=type`: List of component types.
*   `section=question&key=type`: List of question categories.

## 7. Static Resources
*   **Flags**: `GET /flags/{countryCode}.png` returns the flag image for the specified country code (e.g., `cn`, `us`).

## 8. Version Compatibility
*   The API supports client version checks via the `baseVersion` parameter in the updater.
*   Deprecated endpoints: None explicitly found in the active code paths.
