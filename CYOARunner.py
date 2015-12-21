import os
os.system('cls')

f = open('C:\CS\writeFile.csv', 'r')

story = {}

def getTransitions(arr) :
	i = 0
	transitions = {}
	while arr[i] != 'END\n' and arr[i] != 'END' :
		transitions[int(arr[i])] = arr[i+1]
		i+=2
	return transitions
	
def printTransitions(transitions) :
	for t in transitions.keys() :
		print(transitions[t] + " pg. " + str(t))

for line in f :
	splitLine = line.split("#")
	story[int(splitLine[0])] = {'Text': splitLine[1], 'Transitions': getTransitions(splitLine[2:])} 

pageNum = 1;
	
while 1 :
	print(story[pageNum]['Text'] + '\n')
	printTransitions(story[pageNum]['Transitions'])
	try:
		newPage=int(input('\n\nTurn to Page: '))	
	except ValueError:
		print('Not a number')
	if(newPage in story[pageNum]['Transitions']) :
		pageNum = newPage
	os.system('cls')