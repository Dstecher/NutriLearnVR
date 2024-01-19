#!/usr/bin/env python
# coding: utf-8

# In[152]:


#Dict Key: Food Product Name, Value: [#action performed, summed time for action]
grab_dict = {}
add_dict = {}
remove_dict = {}
release_dict = {}

grab_dict_full = {}
add_dict_full = {}
remove_dict_full = {}
release_dict_full = {}

grab_dict_reduced = {}
add_dict_reduced = {}
remove_dict_reduced = {}
release_dict_reduced = {}


# In[96]:


from datetime import datetime
import pprint

def analyze_testdata(filename, activate_print=True):
    added = False
    released = False
    removed = False
    user_info = ""
    selection_info = ""
    selection_summary = ""
    score_info = ""
    current_scene = ""
    time_zero = datetime.strptime('00:00:00.0000', '%H:%M:%S.%f')
    
    with open("../Usertest/logs/" + filename) as f:
        lines = f.readlines()
        
    for i, line in enumerate(lines):
        if "current scene" in line:
            line_words = line.split(" ")
            current_scene = line_words[len(line_words)-1].strip()
        #print(line)
        if "User has closed User UI in start routine with the following information" in line or "The user has a total energy consumption of" in line or "The user self assesses his/her knowledge on nutrition as" in line:
            line_words = line.split(" ")
            for index, word in enumerate(line_words):
                if word in ["Gender:", "Age:", "Weight:", "Height:"]:
                    user_info += line_words[index] + " " + line_words[index+1] + "\n"
                if word == "consumption":
                    user_info += "average energy consumption per day: " + line_words[index+2] + "\n"
                if word == "knowledge":
                    user_info += "self-assessed nutrition knowledge: " + line_words[index+4] + "\n"
        if "User has grabbed food product of category" in line:
            line_words = line.split(" ")
            for index, word in enumerate(line_words):
                timestamp = line_words[1] + " " + line_words[2]
                timestamp = timestamp[:-1] + "00"
                datetime_time = datetime.strptime(timestamp, '%d-%m-%Y %H:%M:%S.%f')
                if (word == "grabbed"):
                    item_name = lines[i+1][12:]
                    item_category = line_words[index+5].strip()
                    #print(datetime_time.strftime('%d-%m-%Y %H:%M:%S.%f') + ": " + "grabbed item category: " + item_category + ", Item: " + item_name)
                    
                    nextline = lines[i+2] + lines[i+3]
                    nextline_words = nextline.split(" ")
                    next_timestamp = nextline_words[1] + " " + nextline_words[2]
                    next_timestamp = next_timestamp[:-1] + "00"
                    #print(next_timestamp)
                    next_datetime = datetime.strptime(next_timestamp, '%d-%m-%Y %H:%M:%S.%f')
                    if "added" in nextline:
                        next_category = nextline_words[11].strip()
                        next_item = nextline[99 + len(next_category):]
                        added = True
                    elif "released" in nextline:
                        next_category = nextline_words[10].strip()
                        next_item = nextline[89 + len(next_category):]
                        released = True
                    elif "removed" in nextline:
                        next_category = nextline_words[11].strip()
                        next_item = nextline[103 + len(next_category):]
                        removed = True
                        
                    #print("Next category: " + next_category)
                    #print("Next item: " + next_item)
                        
                    #print("Nextline: "+ nextline)
                    timedelta = str(next_datetime - datetime_time)[:-3]
                    #print("Time delta: " + timedelta)                    
                    if item_name == next_item and item_category == next_category:
                        #sanitize item name for output:
                        item_name = item_name.strip()
                        if added:
                            selection_info += "Added object " + item_name + " of category " + item_category + " after " + timedelta + "\n"
                            if item_name in add_dict:
                                current_datetime = datetime.strptime(add_dict[item_name][1], '%H:%M:%S.%f')
                                delta_datetime = datetime.strptime(timedelta, '%H:%M:%S.%f')
                                add_dict[item_name] = [add_dict[item_name][0] + 1, str((current_datetime - time_zero + delta_datetime).time())[:-3]]
                            else:
                                add_dict[item_name] = [1, timedelta]
                                
                            if current_scene == "MarketScene":
                                if item_name in add_dict_full:
                                    current_datetime = datetime.strptime(add_dict_full[item_name][1], '%H:%M:%S.%f')
                                    delta_datetime = datetime.strptime(timedelta, '%H:%M:%S.%f')
                                    add_dict_full[item_name] = [add_dict_full[item_name][0] + 1, str((current_datetime - time_zero + delta_datetime).time())[:-3]]
                                else:
                                    add_dict_full[item_name] = [1, timedelta]
                            else:
                                if item_name in add_dict_reduced:
                                    current_datetime = datetime.strptime(add_dict_reduced[item_name][1], '%H:%M:%S.%f')
                                    delta_datetime = datetime.strptime(timedelta, '%H:%M:%S.%f')
                                    add_dict_reduced[item_name] = [add_dict_reduced[item_name][0] + 1, str((current_datetime - time_zero + delta_datetime).time())[:-3]]
                                else:
                                    add_dict_reduced[item_name] = [1, timedelta]
                        if released:
                            #print("Released object " + item_name + " of category " + item_category + " after " + timedelta)
                            ###selection_info += "Released object " + item_name + " of category " + item_category + " after " + timedelta + "\n"
                            if item_name in release_dict:
                                current_datetime = datetime.strptime(release_dict[item_name][1], '%H:%M:%S.%f')
                                delta_datetime = datetime.strptime(timedelta, '%H:%M:%S.%f')
                                release_dict[item_name] = [release_dict[item_name][0] + 1, str((current_datetime - time_zero + delta_datetime).time())[:-3]]
                            else:
                                release_dict[item_name] = [1, timedelta]
                                
                            if current_scene == "MarketScene":
                                if item_name in release_dict_full:
                                    current_datetime = datetime.strptime(release_dict_full[item_name][1], '%H:%M:%S.%f')
                                    delta_datetime = datetime.strptime(timedelta, '%H:%M:%S.%f')
                                    release_dict_full[item_name] = [release_dict_full[item_name][0] + 1, str((current_datetime - time_zero + delta_datetime).time())[:-3]]
                                else:
                                    release_dict_full[item_name] = [1, timedelta]
                            else:
                                if item_name in release_dict_reduced:
                                    current_datetime = datetime.strptime(release_dict_reduced[item_name][1], '%H:%M:%S.%f')
                                    delta_datetime = datetime.strptime(timedelta, '%H:%M:%S.%f')
                                    release_dict_reduced[item_name] = [release_dict_reduced[item_name][0] + 1, str((current_datetime - time_zero + delta_datetime).time())[:-3]]
                                else:
                                    release_dict_reduced[item_name] = [1, timedelta]
                        if removed:
                            #print("Removed object " + item_name + " of category " + item_category + " after " + timedelta)
                            selection_info += "Removed object " + item_name + " of category " + item_category + " after " + timedelta + "\n"
                            if item_name in remove_dict:
                                current_datetime = datetime.strptime(remove_dict[item_name][1], '%H:%M:%S.%f')
                                delta_datetime = datetime.strptime(timedelta, '%H:%M:%S.%f')
                                remove_dict[item_name] = [remove_dict[item_name][0] + 1, str((current_datetime - time_zero + delta_datetime).time())[:-3]]
                            else:
                                remove_dict[item_name] = [1, timedelta]
                                
                            if current_scene == "MarketScene":
                                if item_name in remove_dict_full:
                                    current_datetime = datetime.strptime(remove_dict_full[item_name][1], '%H:%M:%S.%f')
                                    delta_datetime = datetime.strptime(timedelta, '%H:%M:%S.%f')
                                    remove_dict_full[item_name] = [remove_dict_full[item_name][0] + 1, str((current_datetime - time_zero + delta_datetime).time())[:-3]]
                                else:
                                    remove_dict_full[item_name] = [1, timedelta]
                            else:
                                if item_name in remove_dict_reduced:
                                    current_datetime = datetime.strptime(remove_dict_reduced[item_name][1], '%H:%M:%S.%f')
                                    delta_datetime = datetime.strptime(timedelta, '%H:%M:%S.%f')
                                    remove_dict_reduced[item_name] = [remove_dict_reduced[item_name][0] + 1, str((current_datetime - time_zero + delta_datetime).time())[:-3]]
                                else:
                                    remove_dict_reduced[item_name] = [1, timedelta]
                                
                        if item_name in grab_dict:
                            current_datetime = datetime.strptime(grab_dict[item_name][1], '%H:%M:%S.%f')
                            delta_datetime = datetime.strptime(timedelta, '%H:%M:%S.%f')
                            grab_dict[item_name] = [grab_dict[item_name][0] + 1, str((current_datetime - time_zero + delta_datetime).time())[:-3]]
                        else:
                            grab_dict[item_name] = [1, timedelta]
                            
                        if current_scene == "MarketScene":
                            if item_name in grab_dict_full:
                                current_datetime = datetime.strptime(grab_dict_full[item_name][1], '%H:%M:%S.%f')
                                delta_datetime = datetime.strptime(timedelta, '%H:%M:%S.%f')
                                total_time = str((current_datetime - time_zero + delta_datetime).time())
                                total_time_len = len(total_time)
                                if total_time_len == 15:
                                    total_time = total_time[:-3] #cut precision to 3 decimals
                                else:
                                    total_time += "." + "0" * (11 - total_time_len) #add correct number of 0 to contain 3 decimals again (format!)                                
                                grab_dict_full[item_name] = [grab_dict_full[item_name][0] + 1, total_time]
                            else:
                                grab_dict_full[item_name] = [1, timedelta]
                        else:
                            if item_name in grab_dict_reduced:
                                current_datetime = datetime.strptime(grab_dict_reduced[item_name][1], '%H:%M:%S.%f')
                                delta_datetime = datetime.strptime(timedelta, '%H:%M:%S.%f')
                                total_time = str((current_datetime - time_zero + delta_datetime).time())
                                total_time_len = len(total_time)
                                if total_time_len == 15:
                                    total_time = total_time[:-3] #cut precision to 3 decimals
                                else:
                                    total_time += "." + "0" * (11 - total_time_len) #add correct number of 0 to contain 3 decimals again (format!)
                                grab_dict_reduced[item_name] = [grab_dict_reduced[item_name][0] + 1, total_time]
                            else:
                                grab_dict_reduced[item_name] = [1, timedelta]

                added = False
                released = False
                removed = False
                            
        if "User selection data to server" in line:
            selection_info += "\n\nSelection Summary data:\n"
            selection_summary = line[64:]
            selection_info += selection_summary
            
        if "User checks current score" in line:
            score_info += line[75:]
        
    if activate_print:
        equals_counter = 72
        print("=" * equals_counter)
        print("\nCURRENT SCENE: " + current_scene)
        print("=" * equals_counter)
        print("\n===== USER INFORMATION =====")
        print(user_info)
        print("=" * equals_counter)
        print("\n===== SELECTION INFORMATION =====")
        print(selection_info)
        print("=" * equals_counter)
        print("\n===== SCORE INFORMATION =====")
        print(score_info)
        print("=" * equals_counter)
    
    return current_scene, user_info, selection_summary, score_info
    
            
#analyze_testdata("usertest_13_12_23_6.txt")


# In[6]:


def compare_user_runs(run_name1, run_name2):
    run1_scene, run1_user, run1_selection, run1_score = analyze_testdata(run_name1, False)
    run2_scene, run2_user, run2_selection, run2_score = analyze_testdata(run_name2, False)
    
    print("User data: " + run1_user + "\nuser (test): " + run2_user)
    print("First scene name: " + run1_scene + "\nsecond scene (test): " + run2_scene)
    print("First selection data: " + run1_selection)
    print("Second selection data: " + run2_selection)
    print("First score data: " + run1_score)
    print("Second score data: " + run2_score)


# In[ ]:





# In[154]:


equals_counter = 72

print("=" * equals_counter)
print("\n===== GLOBAL FOOD PRODUCTS INFORMATION =====")
print("=" * equals_counter)
print("=" * equals_counter)
print("\n===== GRABBED FOOD PRODUCTS INFORMATION =====")
pprint.pprint(grab_dict)
print("=" * equals_counter)
print("\n===== ADDED FOOD PRODUCTS INFORMATION =====")
pprint.pprint(add_dict)
print("=" * equals_counter)
print("\n===== RELEASED FOOD PRODUCTS INFORMATION =====")
pprint.pprint(release_dict)
print("=" * equals_counter)
print("\n===== REMOVED FOOD PRODUCTS INFORMATION =====")
pprint.pprint(remove_dict)
print("=" * equals_counter)


# In[157]:


equals_counter = 72

print("=" * equals_counter)
print("\n===== MarketScene FOOD PRODUCTS INFORMATION =====")
print("=" * equals_counter)
print("=" * equals_counter)
print("\n===== GRABBED FOOD PRODUCTS INFORMATION =====")
pprint.pprint(grab_dict_full)
print("=" * equals_counter)
print("\n===== ADDED FOOD PRODUCTS INFORMATION =====")
pprint.pprint(add_dict_full)
print("=" * equals_counter)
print("\n===== RELEASED FOOD PRODUCTS INFORMATION =====")
pprint.pprint(release_dict_full)
print("=" * equals_counter)
print("\n===== REMOVED FOOD PRODUCTS INFORMATION =====")
pprint.pprint(remove_dict_full)
print("=" * equals_counter)


# In[158]:


equals_counter = 72

print("=" * equals_counter)
print("\n===== MarketSceneReduced FOOD PRODUCTS INFORMATION =====")
print("=" * equals_counter)
print("=" * equals_counter)
print("\n===== GRABBED FOOD PRODUCTS INFORMATION =====")
pprint.pprint(grab_dict_reduced)
print("=" * equals_counter)
print("\n===== ADDED FOOD PRODUCTS INFORMATION =====")
pprint.pprint(add_dict_reduced)
print("=" * equals_counter)
print("\n===== RELEASED FOOD PRODUCTS INFORMATION =====")
pprint.pprint(release_dict_reduced)
print("=" * equals_counter)
print("\n===== REMOVED FOOD PRODUCTS INFORMATION =====")
pprint.pprint(remove_dict_reduced)
print("=" * equals_counter)


# In[ ]:





# In[62]:


def get_test_info_for_user(user_number):
    if user_number == 1:
        compare_user_runs("usertest_10_12_23.txt", "usertest_10_12_23_2.txt")
    elif user_number == 2:
        compare_user_runs("usertest_11_12_23_11.txt", "usertest_11_12_23_11_2.txt")
    elif user_number == 3:
        compare_user_runs("usertest_11_12_23_3.txt", "usertest_11_12_23_4.txt")
    elif user_number == 4:
        compare_user_runs("user_11_12_23_5.txt", "usertest_11_12_23_6.txt")
    elif user_number == 5:
        compare_user_runs("usertest_11_12_23_7.txt", "usertest_11_12_23_8.txt")
    elif user_number == 6:
        compare_user_runs("usertest_11_12_23_9.txt", "usertest_11_12_23_10.txt")
    elif user_number == 7:
        compare_user_runs("usertest_12_12_23_1.txt", "usertest_12_12_23_2.txt")
    elif user_number == 8:
        compare_user_runs("usertest_12_12_23_3.txt", "usertest_12_12_23_4.txt")
    elif user_number == 9:
        compare_user_runs("usertest_12_12_23_5.txt", "usertest_12_12_23_6.txt")
    elif user_number == 10:
        compare_user_runs("usertest_12_12_23_7.txt", "usertest_12_12_23_8.txt")
    elif user_number == 11:
        compare_user_runs("uesrtest_12_12_23_9.txt", "usertest_12_12_23_10.txt")
    elif user_number == 12:
        compare_user_runs("usertest_13_12_23_1.txt", "usertest_13_12_23_2.txt")
    elif user_number == 13:
        compare_user_runs("usertest_13_12_23_3.txt", "usertest_13_12_23_4.txt")
    elif user_number == 14:
        compare_user_runs("usertest_13_12_23_5.txt", "usertest_13_12_23_6.txt")
    elif user_number == 15:
        compare_user_runs("usertest_18_12_23.txt", "usertest_18_12_23_2.txt")


# In[151]:


analyze_testdata("usertest_18_12_23_2.txt")


# In[111]:


# Load specific user test data:

get_test_info_for_user(1)


# In[153]:


# Load all user test data at once:

for i in reversed(range(1, 16)):
    print("PRINTING USER TEST INFO FOR USER #" + str(i))
    get_test_info_for_user(i)
    print("#" * 72)
    print("#" * 72)


# In[ ]:




