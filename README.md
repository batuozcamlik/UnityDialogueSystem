# UnityDialogueSystem
A lightweight and flexible 2D dialogue system built with Unity. Perfect for narrative-driven games, RPGs, and visual novels.

# 🎮 DialogueFlow2D

**DialogueFlow2D** is a lightweight, modular, and flexible **2D dialogue system for Unity** — perfect for story-driven games, RPGs, and visual novels.  
It offers full control over dialogue text, speaker visuals, audio reactions, and event triggers, all directly from the Unity Editor.

---

## ✨ Features

### 🧩 Flexible Dialogue Framework
- Manage dialogues sequentially or dynamically.
- Add and start new dialogue lists at runtime via `AddDialogueAndStart()`.

### ⌨️ Typewriter Text Effect
- Characters appear one by one with adjustable typing speed.
- Define **custom wait keys** (e.g., “.” or “,”) for pauses of different durations.
- Instantly complete the current dialogue line when skipping.

### 🧠 Built-in Event System
- Trigger **Unity Events** after each dialogue line.
  - Ideal for starting quests, scene transitions, animations, or scripted events.

### 🎭 Dynamic Character Portraits
- Display character portraits on either the left or right side.
- Automatically switch active portraits based on the speaker.
- Smooth transitions powered by **DOTween** (fade and scale effects).

### 🎞️ Animated Dialogue Box
- Dialogue boxes open and close with fade and scale animations.
- Character portraits fade in/out dynamically.

### 🔊 Audio Integration
- Play **reaction sound effects** (`reactionSFX`) for each dialogue line.
- Support for **typing SFX** while text appears on screen.

### 🎨 Editor-Friendly & Customizable
- Configure everything in the **Unity Inspector** — no coding required.
- Supports custom UI layouts, fonts, colors, and animations.

---

## 🧱 Data Structures

### `CharacterInformation`
| Field | Type | Description |
|-------|------|-------------|
| `name` | `string` | Name of the speaking character. |
| `sprite` | `Sprite` | Character portrait image. |
| `text` | `string` | Dialogue line text. |
| `isRight` | `bool` | Whether the portrait appears on the right side. |
| `reactionSFX` | `AudioClip` | Optional reaction sound. |
| `finishEvent` | `UnityEvent` | Event triggered after the line finishes. |

### `KeyDuration`
| Field | Type | Description |
|-------|------|-------------|
| `key` | `string` | Special symbol that triggers a typing pause. |
| `duration` | `float` | Wait time when this key appears. |

---

## 🛠 Requirements
- **Unity 2020 or later**
- **TextMeshPro**
- **DOTween (Demigiant)**

---

## 💡 Tips
- Use `KeyDuration` to create natural dialogue pacing.  
  *(Example: “.” = 0.3s, “,” = 0.15s)*  
- Adjust `skipDialoguesKey` to change the skip input (default: `Space`).  
- Modify `animationSpeed` to control transition timing.

---

## 🧾 License
This project is open-source and free to use in both personal and commercial Unity projects.

