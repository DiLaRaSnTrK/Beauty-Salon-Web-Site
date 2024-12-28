from flask import Flask, request, jsonify
import cv2
import numpy as np

app = Flask(__name__)

@app.route('/analyze', methods=['POST'])
def analyze():
    if 'file' not in request.files:
        return jsonify({"error": "No file uploaded"}), 400

    file = request.files['file']
    npimg = np.fromfile(file, np.uint8)
    img = cv2.imdecode(npimg, cv2.IMREAD_COLOR)

    # Face detection
    face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + 'haarcascade_frontalface_default.xml')
    gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    faces = face_cascade.detectMultiScale(gray, 1.1, 4)

    if len(faces) == 0:
        return jsonify({"error": "No face detected"}), 400

    for (x, y, w, h) in faces:
        face_region = img[y:y+h, x:x+w]
        avg_color_bgr = np.mean(face_region, axis=(0, 1))
        avg_color = avg_color_bgr[::-1]  # Convert BGR to RGB
        print(f"Average color: {avg_color}")

        # Hair color suggestion based on average color
        if avg_color[0] > 200 and avg_color[1] > 180 and avg_color[2] > 170:  # Light tones
            suggestion = "Platinum Blonde"
        elif avg_color[0] > 180 and avg_color[1] > 140 and avg_color[2] > 120:  # Golden tones
            suggestion = "Golden Blonde"
        elif avg_color[0] > 160 and avg_color[1] > 120 and avg_color[2] < 100:  # Strawberry Blonde
            suggestion = "Strawberry Blonde"
        elif avg_color[0] > 140 and avg_color[1] > 100 and avg_color[2] > 100:  # Ash Brown
            suggestion = "Ash Brown"
        elif avg_color[0] > 120 and avg_color[1] < 100 and avg_color[2] < 100:  # Copper Red
            suggestion = "Copper Red"
        elif avg_color[0] < 100 and avg_color[1] > 120 and avg_color[2] > 120:  # Mahogany Brown
            suggestion = "Mahogany Brown"
        elif avg_color[0] < 80 and avg_color[1] < 80 and avg_color[2] < 80:  # Dark tones
            suggestion = "Natural Black"
        elif avg_color[0] > 130 and avg_color[1] > 90 and avg_color[2] > 70:  # Chestnut tones
            suggestion = "Chestnut Brown"
        elif avg_color[0] > 150 and avg_color[1] > 100 and avg_color[2] < 90:  # Auburn
            suggestion = "Auburn"
        elif avg_color[0] > 100 and avg_color[1] > 70 and avg_color[2] > 50:  # Honey Blonde
            suggestion = "Honey Blonde"
        else:
            suggestion = "Neutral Brown"

        return jsonify({"suggestion": suggestion, "avg_color": avg_color.tolist()})

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
