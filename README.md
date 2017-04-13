# ObserverSignalR
Exemplo de Projeto utilizando o SignalR para simular um observer

# Composição do Projeto
- GatewayCommunication
  > Serviço Windows responsável por disponibilizar Hub SignalR para comunicação com os clientes
- ListenUDP
  > Serviço Windows responsável por ouvir uma comunicação UDP.
    Este serviço repassa a mensagem recebida via UDP para o GatewayCommunication. 
- Send/SendUDP
  > Serviço Windows responsável por enviar mensagens via UDP
- ObserverHubClients/ObserverHubClientA
  > Serviço Windows responsável por simular um cliente que ouça métodos do hub GatewayCommunication, no grupo "udp"
- ObserverHubClients/ObserverHubClientB
    > Serviço Windows responsável por simular um cliente que ouça métodos do hub             GatewayCommunication, no grupo "other"

### Observação (serão retirados em momento oportuno)
Estes dois projetos fazem parte da mesma solution, porém não compartilham a teconologia SignalR
- ListenTCP
    > Serviço Windows responsável por ouvir uma comunicação TCP.
- Send/SendTCP
    > Serviço Windows responsável por enviar mensagens via TCP
