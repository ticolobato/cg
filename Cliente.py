import socket

IP = input("Endereço IP do servidor (geralmente 127.0.0.1): ") # Endereço IP do servidor
PORT = input("Insira a porta de comunicação: ")   # Porta que o Servidor esta

conexao = socket.socket()
dest = (IP,int(PORT)) # Destinatario
# print ('Para sair use CTRL+X\n')

conexao.connect(dest)
print ('Conectado ao servidor')
print ('Insira sua requisição:')
mensagem = input()

while mensagem != '\x18':

    conexao.send(str.encode(mensagem)) # Enviar para Servidor
    data = conexao.recv(1024) # Recebido do servidor
    print("Recebido do servidor: {}".format(data.decode())) # Informaçao do servidor decodificada
    mensagem = input() # Nova mensagem

conexao.close()

