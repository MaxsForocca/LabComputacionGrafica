import cv2
import numpy as np
import os

# Configuración inicial del lienzo blanco
lienzo = np.ones((600, 800, 3), dtype="uint8") * 255
historial = [lienzo.copy()]  # Historial para la función deshacer

# Variables globales de control
dibujando = False
modo_figura = 'c'  # 'c' para círculo, 'r' para rectángulo
ix, iy = -1, -1
cx, cy = -1, -1   # Coordenadas actuales del mouse mientras se arrastra

print("Controles del lienzo:")
print("'c' -> Modo Círculo\n'r' -> Modo Rectángulo\n'z' -> Deshacer\n's' -> Guardar Dibujo\n'ESC' -> Salir\n" + "-"*30)

def dibujar(event, x, y, flags, param):
    global ix, iy, cx, cy, dibujando, lienzo, historial, modo_figura
    
    if event == cv2.EVENT_LBUTTONDOWN:
        dibujando = True
        ix, iy = x, y
        cx, cy = x, y # Inicializar la posición actual
        
    elif event == cv2.EVENT_MOUSEMOVE:
        if dibujando:
            cx, cy = x, y # Solo actualizamos las coordenadas actuales
            
    elif event == cv2.EVENT_LBUTTONUP:
        dibujando = False
        # Guardamos el estado actual en el historial ANTES de modificar el lienzo
        historial.append(lienzo.copy())
        
        # Consolidar el dibujo definitivo en el lienzo real
        if modo_figura == 'r':
            cv2.rectangle(lienzo, (ix, iy), (x, y), (255, 0, 0), 2)
        elif modo_figura == 'c':
            radio = int(((x - ix)**2 + (y - iy)**2)**0.5)
            cv2.circle(lienzo, (ix, iy), radio, (0, 0, 255), 2)

# Vincular la ventana con la función del mouse
cv2.namedWindow("Pizarra Interactiva")
cv2.setMouseCallback("Pizarra Interactiva", dibujar)

while True:
    # Si estamos arrastrando el mouse, mostramos la previsualización en tiempo real
    if dibujando:
        copia_temporal = lienzo.copy()
        if modo_figura == 'r':
            cv2.rectangle(copia_temporal, (ix, iy), (cx, cy), (255, 0, 0), 2)
        elif modo_figura == 'c':
            radio = int(((cx - ix)**2 + (cy - iy)**2)**0.5)
            cv2.circle(copia_temporal, (ix, iy), radio, (0, 0, 255), 2)
        
        cv2.imshow("Pizarra Interactiva", copia_temporal)
    else:
        # Si no se está dibujando, se muestra el lienzo definitivo estable
        cv2.imshow("Pizarra Interactiva", lienzo)
    
    tecla = cv2.waitKey(1) & 0xFF
    
    if tecla == ord('r'):
        modo_figura = 'r'
        print("Modo cambiado a: Rectángulo")
    elif tecla == ord('c'):
        modo_figura = 'c'
        print("Modo cambiado a: Círculo")
    elif tecla == ord('z'):
        if len(historial) > 0:
            lienzo = historial.pop()  # Recupera el último estado guardado
            print("Deshacer aplicado.")
        else:
            print("No hay más acciones para deshacer.")
    elif tecla == ord('s'):
        # Validar existencia de la carpeta para evitar crashes
        if not os.path.exists("resultados"):
            os.makedirs("resultados")
        cv2.imwrite("resultados/dibujo_final.png", lienzo)
        print("Dibujo guardado exitosamente en 'resultados/dibujo_final.png'.")
    elif tecla == 27: # ESC
        break

cv2.destroyAllWindows()