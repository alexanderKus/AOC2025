const NUM_TICKS = 100;
const STEP_DURATION_MS = 60; // How long each step is visible

type Instruction = {
    dir: "L" | "R";
    steps: number;
};

const instructions: Instruction[] = [
    { dir: "L", steps: 68 },
    { dir: "L", steps: 30 },
    { dir: "R", steps: 48 },
    { dir: "L", steps: 5 },
    { dir: "R", steps: 60 },
    { dir: "L", steps: 55 },
    { dir: "L", steps: 1 },
    { dir: "L", steps: 99 },
    { dir: "R", steps: 14 },
    { dir: "L", steps: 82 },
];

const canvas = document.getElementById("clock") as HTMLCanvasElement | null;
const infoEl = document.getElementById("info") as HTMLDivElement | null;

if (!canvas) {
    throw new Error("Canvas element #clock not found");
}

const ctx = canvas.getContext("2d");
if (!ctx) {
    throw new Error("2D context not available");
}

const width = canvas.width;
const height = canvas.height;
const cx = width / 2;
const cy = height / 2;
const radius = Math.min(width, height) * 0.4;

function expandInstructions(start: number, ins: Instruction[]): number[] {
    let pos = start;
    const positions: number[] = [pos];

    for (const inst of ins) {
        const delta = inst.dir === "R" ? 1 : -1;
        for (let i = 0; i < inst.steps; i++) {
            pos = (pos + delta + NUM_TICKS) % NUM_TICKS;
            positions.push(pos);
        }
    }
    return positions;
}

const positions = expandInstructions(0, instructions);

/**
 * Draw the clock face (0-99 ticks, labels every 5, bold label every 10).
 */
function drawClockFace() {
    ctx.save();
    ctx.clearRect(0, 0, width, height);

    // Outer circle
    ctx.beginPath();
    ctx.arc(cx, cy, radius, 0, Math.PI * 2);
    ctx.lineWidth = 2;
    ctx.strokeStyle = "#555";
    ctx.stroke();

    // Ticks and labels
    for (let i = 0; i < NUM_TICKS; i++) {
        const angle = (i / NUM_TICKS) * Math.PI * 2 - Math.PI / 2;

        const isMajor10 = i % 10 === 0;
        const isMinor5 = !isMajor10 && i % 5 === 0;

        const tickOuter = radius;
        const tickInner = tickOuter - (isMajor10 ? 18 : isMinor5 ? 12 : 6);

        const cos = Math.cos(angle);
        const sin = Math.sin(angle);

        const xOuter = cx + cos * tickOuter;
        const yOuter = cy + sin * tickOuter;
        const xInner = cx + cos * tickInner;
        const yInner = cy + sin * tickInner;

        ctx.beginPath();
        ctx.moveTo(xInner, yInner);
        ctx.lineTo(xOuter, yOuter);
        ctx.lineWidth = isMajor10 ? 2 : 1;
        ctx.strokeStyle = isMajor10 ? "#fff" : "#666";
        ctx.stroke();

        // Labels every 10
        if (isMajor10) {
            const labelRadius = tickInner - 16;
            const lx = cx + cos * labelRadius;
            const ly = cy + sin * labelRadius;
            ctx.font = "12px system-ui, sans-serif";
            ctx.textAlign = "center";
            ctx.textBaseline = "middle";
            ctx.fillStyle = "#ddd";
            ctx.fillText(i.toString(), lx, ly);
        }
    }

    ctx.restore();
}

/**
 * Draw the pointer at a given tick index.
 */
function drawPointer(index: number) {
    const angle = (index / NUM_TICKS) * Math.PI * 2 - Math.PI / 2;
    const pointerRadius = radius - 30;

    const px = cx + Math.cos(angle) * pointerRadius;
    const py = cy + Math.sin(angle) * pointerRadius;

    // Pointer line
    ctx.save();
    ctx.beginPath();
    ctx.moveTo(cx, cy);
    ctx.lineTo(px, py);
    ctx.lineWidth = 3;
    ctx.strokeStyle = "#0ff";
    ctx.stroke();

    // Pointer head circle
    ctx.beginPath();
    ctx.arc(px, py, 8, 0, Math.PI * 2);
    ctx.fillStyle = "#0ff";
    ctx.fill();

    // Center hub
    ctx.beginPath();
    ctx.arc(cx, cy, 6, 0, Math.PI * 2);
    ctx.fillStyle = "#0ff";
    ctx.fill();

    ctx.restore();

    // Show current index in the center
    ctx.save();
    ctx.font = "bold 32px system-ui, sans-serif";
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillStyle = "#fff";
    ctx.fillText(index.toString(), cx, cy);
    ctx.restore();

    // Update info text
    if (infoEl) {
        infoEl.textContent = `Current position: ${index}   ·   Step ${
            currentStepIndex
        } / ${positions.length - 1}`;
    }
}

let currentStepIndex = 0;
let lastTimestamp = 0;

/**
 * Main animation loop.
 */
function animate(timestamp: number) {
    if (!lastTimestamp) {
        lastTimestamp = timestamp;
    }

    const elapsed = timestamp - lastTimestamp;
    const stepsToAdvance = Math.floor(elapsed / STEP_DURATION_MS);

    if (stepsToAdvance > 0) {
        lastTimestamp += stepsToAdvance * STEP_DURATION_MS;
        currentStepIndex = Math.min(
            currentStepIndex + stepsToAdvance,
            positions.length - 1
        );
    }

    drawClockFace();
    drawPointer(positions[currentStepIndex]);

    if (currentStepIndex < positions.length - 1) {
        requestAnimationFrame(animate);
    } else {
        // Final frame stays on screen
        if (infoEl) {
            infoEl.textContent += "   ·   Animation complete.";
        }
    }
}

// Initial draw
drawClockFace();
drawPointer(positions[0]);
requestAnimationFrame(animate);
